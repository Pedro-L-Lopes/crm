using CRM.Domain.Entities;

namespace CRM.Domain.Repositories.Tenant;
public interface ITenantReadOnlyRepository
{
    public Task<bool> ExistActiveTenantWithEmail(string email);
    public Task<bool> ExistActiveTenant(Guid Id);
    public Task<Entities.Tenant?> GetTenantById(Guid Id);
}
