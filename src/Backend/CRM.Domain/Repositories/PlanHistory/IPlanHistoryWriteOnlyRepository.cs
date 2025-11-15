namespace CRM.Domain.Repositories.PlanHistory;
public interface IPlanHistoryWriteOnlyRepository
{
    public Task Add(Entities.PlanHistory planHistory);
}
