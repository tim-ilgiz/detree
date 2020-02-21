using System;
using System.Security.Claims;
using Application.Common.Interfaces;
using IdentityServer4.Models;

namespace Infrastructure.Authorities
{
    public class AuthenticationAuthority : IAuthenticator
    {
        private static readonly string authSecret = "authenticationsecretkey".Sha256();

        public Claim[] GetAuthenticationClaims(string identifier)
        {
            if (!Guid.TryParse(identifier, out var guid))
                throw new FormatException();
            var hash = string.Format("{0}:{1}", identifier, authSecret).Sha256();
            return new[]
            {
                new Claim("auth_key", identifier),
                new Claim("auth_hash", hash)
            };
        }
    }
}