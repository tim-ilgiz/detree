using System.Security.Claims;
using Application.Authority;

namespace Application.Common.Interfaces
{
    public interface IAuthority
    {
        string[] Payload { get; }
        Claim[] OnVerify(Claim[] claims, Payload payload, string identifier, out bool valid);
        Claim[] OnForward(Claim[] claims);
    }

    public interface IAuthenticator
    {
        Claim[] GetAuthenticationClaims(string identifier);
    }
}