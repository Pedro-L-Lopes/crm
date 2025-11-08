using CRM.Domain.Entities;
using CRM.Domain.Repositories.Plan;
using CRM.Domain.Repositories.Tenant;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.DataAccess.Repositories;

public class PlanRepository : IPlanWriteOnlyRepository, IPlanReadOnlyRepository
{
    private readonly CRMDbContext _dbContext;

    public PlanRepository(CRMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Plan plan)
    {
        await _dbContext.Plans.AddAsync(plan);
    }

    public async Task<Plan> GetPlanById(Guid id)
    {
        var plan = await _dbContext.Plans
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

        return plan;
    }

    public async Task<bool> ExistsAndIsActiveAsync(Guid id)
    {
        return await _dbContext.Plans.AnyAsync(p => p.Id == id && p.IsActive);
    }

}
