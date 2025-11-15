using CRM.Domain.Entities;

namespace CRM.Domain.Repositories.Tenant;
public interface ITenantWriteOnlyRepository
{
    public Task Add(Entities.Tenant tenant);
}
