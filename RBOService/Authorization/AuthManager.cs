using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RBOService.Infrastructure
{
    public class AuthManager 
    {
        private readonly AuthConfig _authConfig;
        private readonly byte[] _secret;

        public AuthManager(AuthConfig authConfig)
        {
            _authConfig = authConfig;
            _secret = Encoding.ASCII.GetBytes(authConfig.Secret);
        }

        public string GenerateTokens(Claim[] claims)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);

            var jwtToken = new JwtSecurityToken(
                _authConfig.Issuer,
                shouldAddAudienceClaim ? _authConfig.Audience : string.Empty,
                claims,
                expires: DateTime.Now.AddMinutes(_authConfig.Expiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return accessToken;
           
        }
    }
}
