using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface IAuthority
    {
        string[] Payload { get; }
        Claim[] OnVerify(Claim[] claims, JObject payload, string identifier, out bool valid);
        Claim[] OnForward(Claim[] claims);
    }
    
    public interface IAuthenticator
    {
        Claim[] GetAuthenticationClaims(string identifier);
    }
}
