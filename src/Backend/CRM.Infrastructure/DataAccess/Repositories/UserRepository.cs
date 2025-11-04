using CRM.Domain.Entities;
using CRM.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private readonly CRMDbContext _dbContext;

    public UserRepository(CRMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.IsActive);
    }
}
