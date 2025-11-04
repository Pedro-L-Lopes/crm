using CRM.Domain.Entities;
using CRM.Domain.Repositories.Tenant;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.DataAccess.Repositories;

public class TenantRepository : ITenantWriteOnlyRepository, ITenantReadOnlyRepository
{
    private readonly CRMDbContext _dbContext;

    public TenantRepository(CRMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Tenant tenant)
    {
        await _dbContext.Tenants.AddAsync(tenant);
    }

    public async Task<bool> ExistActiveTenant(Guid id)
    {
        return await _dbContext.Tenants.AnyAsync(tenant => tenant.Id == id);
    }

    public async Task<bool> ExistActiveTenantWithEmail(string email)
    {
        return await _dbContext.Tenants.AnyAsync(tenant => tenant.Email.Equals(email) && tenant.IsActive);
    }
}
