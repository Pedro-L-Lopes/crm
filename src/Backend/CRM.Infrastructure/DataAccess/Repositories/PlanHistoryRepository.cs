using CRM.Domain.Entities;
using CRM.Domain.Repositories.PlanHistory;

namespace CRM.Infrastructure.DataAccess.Repositories;
public class PlanHistoryRepository : IPlanHistoryWriteOnlyRepository, IPlanHistoryReadOnlyRepository
{
    private readonly CRMDbContext _dbContext;

    public PlanHistoryRepository(CRMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(PlanHistory planHistory)
    {
        await _dbContext.PlanHistories.AddAsync(planHistory);
    }
}
