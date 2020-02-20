using Application.Common.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Application.Authority;

namespace Infrastructure.Authorities
{
    public class AccountAuthority : IAuthority
    {
        private IAccountRepository _repository;

        public AccountAuthority(IAccountRepository repository)
        {
            _repository = repository;
        }

        public string[] Payload => new string[] { "username", "password" };

        public Claim[] OnForward(Claim[] claims)
        {
            throw new NotImplementedException();
        }

        public Claim[] OnVerify(Claim[] claims, Payload payload, string identifier, out bool valid)
        {
            valid = false;
            var user = _repository.GetAccount(payload.Username, payload.Password);
            if (user == null)
                throw new KeyNotFoundException();
            valid = true;
            return new Claim[]
            {
          new Claim(identifier, user.UserGuid.ToString()),
          new Claim("phone", user.Phone)
            };
        }
    }
}
