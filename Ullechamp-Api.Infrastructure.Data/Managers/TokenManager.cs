using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Infrastructure.Data.Managers
{
    public class TokenManager
    {
        private readonly string _jwtKey;
        private readonly double _jwtExpireDays;
        private readonly string _jwtIssuer;

        public TokenManager(string jwtKey, double jwtExpireDays, string jwtIssuer)
        {
            _jwtKey = jwtKey;
            _jwtExpireDays = jwtExpireDays;
            _jwtIssuer = jwtIssuer;
        }
        
        public string GenerateJwtToken(User user, string accessToken)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("twitchName", user.Twitchname),
                new Claim("role", user.Role),
                new Claim("id", user.Id.ToString()),
                new Claim("accessToken", accessToken)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_jwtExpireDays);

            var token = new JwtSecurityToken(
                _jwtIssuer,
                // TODO: Add valid audience
                null,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}