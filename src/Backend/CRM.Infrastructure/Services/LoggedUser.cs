using CRM.Domain.Entities;
using CRM.Domain.Security.Tokens;
using CRM.Domain.Services.LoggedUser;
using CRM.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace CRM.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly CRMDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(CRMDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value;

        var userIdentifier = Guid.Parse(identifier);

        //return await _dbContext
        //    .Users
        //    .AsNoTracking()
        //    .FirstAsync(user => user.IsActive && user.Id == userIdentifier);

        return await _dbContext
            .Users
            .Include(user => user.Tenant)
                .ThenInclude(tenant => tenant!.Plan)
            .FirstAsync(user => user.IsActive && user.Id == userIdentifier);
    }
    
}