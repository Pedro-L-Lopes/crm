using CRM.Domain.Entities;

namespace CRM.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    //string Generate(User user, Tenant tenant, Plan plan);
    string Generate(User user, Tenant tenant);
}
