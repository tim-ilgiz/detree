﻿using Application.Common.Interfaces;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authorities
{
    public class AuthenticationAuthority : IAuthenticator
    {
        private static string authSecret = "authenticationsecretkey".Sha256();

        public Claim[] GetAuthenticationClaims(string identifier)
        {
            if (!Guid.TryParse(identifier, out Guid guid))
                throw new FormatException();
            var hash = string.Format("{0}:{1}", identifier, authSecret).Sha256();
            return new Claim[]
            {
            new Claim("auth_key", identifier),
            new Claim("auth_hash", hash)
            };
        }
    }
}
