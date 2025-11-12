using CRM.Domain.Entities;
using CRM.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository, IUserUpdateOnlyRepository
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

    public async Task<User?> GetUserByEmailAndPassword(string email, string password)
    {
        return await _dbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.IsActive && user.Email.Equals(email) && user.Password.Equals(password));
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await _dbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.IsActive && user.Id.Equals(id));
    }

    public async Task<bool> ExistsUserActiveWithIdentifier(Guid id)
    {
        return await _dbContext.Users.AnyAsync(user => user.Id.Equals(id) && user.IsActive);
    }

    public async Task<User?> GetUserByIdentifier(Guid id)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id.Equals(id) && user.IsActive);
    }

    public async Task<User> GetById(Guid id)
    {
        return await _dbContext
            .Users
            .FirstAsync(user => user.Id == id);
    }

    public void Update(User user) => _dbContext.Users.Update(user);
}
