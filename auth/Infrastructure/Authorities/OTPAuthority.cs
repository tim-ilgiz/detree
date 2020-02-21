using System;
using System.Linq;
using System.Security.Claims;
using Application.Authority;
using Application.Common.Interfaces;
using IdentityServer4.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Authorities
{
    public class OTPAuthority : IAuthority
    {
        private readonly ILogger _logger;

        public OTPAuthority(ILogger logger)
        {
            _logger = logger;
        }

        public string[] Payload => new[] {"otp"};

        public Claim[] OnForward(Claim[] claims)
        {
            var phone = claims.Single(c => c.Type == "phone").Value;
            return generateOTPClaims(phone);
        }

        public Claim[] OnVerify(Claim[] claims, Payload payload, string identifier, out bool valid)
        {
            valid = false;
            var id = claims.Single(c => c.Type == identifier).Value;
            var otpId = claims.Single(c => c.Type == "otp_id").Value;
            var hash = claims.Single(c => c.Type == "otp_hash").Value;
            if (string.Format("{0}:{1}", otpId, payload.Otp).Sha256() == hash)
            {
                valid = true;
                return new[]
                {
                    new Claim(identifier, id)
                };
            }

            throw new ArgumentException();
        }

        private Claim[] generateOTPClaims(string phone)
        {
            var digit = 4;
            var otp = new Random().Next(0, (int) Math.Pow(10, digit) - 1).ToString("####");
            var msg = string.Format("Phone number {0} OTP is {1}", phone, otp);
            _logger.LogInformation(string.Format("\n{0}\n{1}\n{0}\n", new string('*', msg.Length), msg));
            var sid = DateTime.Now.Ticks.ToString();

            var hash = string.Format("{0}:{1}", sid, otp).Sha256();
            return new[]
            {
                new Claim("otp_id", sid),
                new Claim("otp_hash", hash)
            };
        }
    }
}