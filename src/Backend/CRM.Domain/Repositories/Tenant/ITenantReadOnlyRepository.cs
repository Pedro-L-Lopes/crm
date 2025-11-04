namespace CRM.Domain.Repositories.Tenant;
public interface ITenantReadOnlyRepository
{
    public Task<bool> ExistActiveTenantWithEmail(string email);
    public Task<bool> ExistActiveTenant(Guid Id);
}
