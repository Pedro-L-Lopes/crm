namespace CRM.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    public Task<bool> ExistActiveUserWithEmail(string email);
    public Task<Entities.User?> GetUserByEmailAndPassword(string email, string password);
    public Task<bool> ExistsUserActiveWithIdentifier(Guid identifier);
    Task<Entities.User?> GetUserByIdentifier(Guid userIdentifier);
}
