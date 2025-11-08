using CRM.Domain.Entities;
using CRM.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;

namespace CRM.Infrastructure.Security.Access.Generator;
internal class JwtTokenGenerator : IAccessTokenGenerator
{
    private readonly uint _expirationTimeMinutes;
    private readonly string _signingKey;

    public JwtTokenGenerator(uint expirationTimeMinutes, string signingKey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _signingKey = signingKey;
    }

    //public string Generate(User user, Tenant tenant, Plan plan)
    public string Generate(User user, Tenant tenant)
    {
        var claims = new List<Claim>
            {
                // --- ESSENCIAL (para o Backend) ---
                // O ID do usuário. Padrão JWT para "Subject" (Sujeito).
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                
                // O ID da empresa/tenant. Essencial para autorização em multi-tenant.
                new Claim("tenant_id", tenant.Id.ToString()),
                
                // A permissão do usuário. Essencial para [Authorize(Roles = "...")]
                new Claim(ClaimTypes.Role, user.Role),


                // --- DADOS DE EXIBIÇÃO (Movidos para o JSON) ---
                // O frontend precisa disso na tela, mas o backend não precisa 
                // para autorizar cada requisição.
                
                new Claim("plan_expiration", tenant.PlanExpiration.HasValue ? tenant.PlanExpiration.Value.ToString("O") : string.Empty)

                // --- Outros IDs (se necessários para lógica de plano) ---
                // Se o backend precisar checar o ID do plano a cada request,
                // você poderia mantê-lo. Se não, mova para o JSON.
                // new Claim("plan_id", plan.Id.ToString()),
            };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var bytes = Encoding.UTF8.GetBytes(_signingKey);

        return new SymmetricSecurityKey(bytes);
    }
}
