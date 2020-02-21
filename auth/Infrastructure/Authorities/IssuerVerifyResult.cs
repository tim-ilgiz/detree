using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Application.Authority;
using Application.Common.Helpers;
using Application.Common.Interfaces;

namespace Infrastructure.Authorities
{
    public class IssuerVerifyResult
    {
        public string Token { get; set; }
        public string Authority { get; set; }
        public string[] Payload { get; set; }
    }

    public class AuthorityIssuer
    {
        private readonly IAuthenticator _authenticator;
        private readonly string _identifier;
        private readonly IDictionary<string, int> _timeouts = new Dictionary<string, int>();

        public AuthorityIssuer(IAuthenticator authenticator, string identifier)
        {
            _authenticator = authenticator;
            _identifier = identifier;
        }

        public IDictionary<string, IAuthority> Authorities { get; } = new Dictionary<string, IAuthority>();

        public static AuthorityIssuer Create(IAuthenticator authenticator, string identifier)
        {
            return new AuthorityIssuer(authenticator, identifier);
        }

        internal void Register(IAuthority authority, string name, int timeout)
        {
            Authorities.Add(name, authority);
            _timeouts.Add(name, timeout);
        }

        public IssuerVerifyResult Verify(string authority, Claim[] claims, Payload payload)
        {
            var verifyAuthority = Authorities[authority];
            var verifyClaims = verifyAuthority.OnVerify(claims, payload, _identifier, out var valid);
            var authorities = Authorities.Values.ToList();
            var idx = authorities.IndexOf(verifyAuthority);

            if (idx < Authorities.Count - 1)
            {
                var nextAuthority = authorities[idx + 1];
                var forwardClaims = new Claim[] { };
                var forwardAuthority = Authorities.Keys.ElementAt(idx + 1);
                var forwardPayload = nextAuthority.Payload;
                if (verifyClaims.Any()) forwardClaims = nextAuthority.OnForward(verifyClaims);
                if (valid)
                {
                    var verifyToken = JwtHelper.GenerateToken(verifyClaims.Concat(forwardClaims).ToArray(),
                        _timeouts[authority]);
                    return new IssuerVerifyResult
                    {
                        Token = verifyToken,
                        Authority = forwardAuthority,
                        Payload = forwardPayload
                    };
                }
            }
            else
            {
                if (valid)
                {
                    var identifier = verifyClaims.SingleOrDefault(c => c.Type == _identifier);
                    var authenticationClaims = _authenticator.GetAuthenticationClaims(identifier.Value);
                    return new IssuerVerifyResult
                    {
                        Token = JwtHelper.GenerateToken(authenticationClaims, _timeouts[authority]),
                        Authority = null,
                        Payload = null
                    };
                }
            }

            var token = JwtHelper.GenerateToken(verifyClaims, _timeouts[authority]);
            return new IssuerVerifyResult
            {
                Token = token,
                Authority = authority,
                Payload = verifyAuthority.Payload
            };
        }
    }

    public static class AuthorityIssuerExtension
    {
        public static AuthorityIssuer Register(this AuthorityIssuer issuer, string name, IAuthority authority,
            int timeout = 60)
        {
            issuer.Register(authority, name, timeout);
            return issuer;
        }
    }
}