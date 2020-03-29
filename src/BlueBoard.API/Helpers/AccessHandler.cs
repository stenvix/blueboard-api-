using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlueBoard.API.Options;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Module.Identity.Helpers;
using Dawn;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BlueBoard.API.Helpers
{
    public class AccessHandler : IAccessHandler
    {
        private readonly IOptionsMonitor<JwtOptions> options;

        public AccessHandler(IOptionsMonitor<JwtOptions> options)
        {
            this.options = options;
        }
        public AccessTokenModel CreateAccessToken(long userId, string email)
        {
            Guard.Argument(userId).NotDefault();
            Guard.Argument(email).NotEmpty();

            var now = DateTime.UtcNow;

            var jwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, this.ToTimestamp(now).ToString()),
            };

            var currentOptions = this.options.CurrentValue;
            var expires = now.AddMinutes(currentOptions.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: currentOptions.Issuer,
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: this.GetSigningCredentials(currentOptions.SecretKey)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AccessTokenModel(token, this.ToTimestamp(expires));
        }

        private SigningCredentials GetSigningCredentials(string secretKey)
        {
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            return new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
        }

        private long ToTimestamp(DateTime dateTime)
        {
            var centuryBegin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expectedDate = dateTime.Subtract(new TimeSpan(centuryBegin.Ticks));

            return expectedDate.Ticks / 10000;
        }
    }
}
