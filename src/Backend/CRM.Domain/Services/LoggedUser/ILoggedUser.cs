using CRM.Domain.Entities;

namespace CRM.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    public Task<User> User();
}