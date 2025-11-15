using CRM.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Infrastructure.Security.Access.Validator
{
    internal class JwtTokenValidator : JwtTokenHandler, IAccessTokenValidator
    {
        private readonly string _signingKey;

        public JwtTokenValidator(string signingKey) => _signingKey = signingKey;

        public Guid ValidateAndGetUserIdentifier(string token)
        {
            var validationParameter = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = SecurityKey(_signingKey),
                ClockSkew = new TimeSpan(0)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            tokenHandler.InboundClaimTypeMap.Clear();

            var principal = tokenHandler.ValidateToken(token, validationParameter, out _);

            var subClaim = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);

            if (subClaim == null)
            {
                throw new SecurityTokenException("Claim 'sub' não encontrada no token");
            }

            return Guid.Parse(subClaim.Value);
        }
    }
}
