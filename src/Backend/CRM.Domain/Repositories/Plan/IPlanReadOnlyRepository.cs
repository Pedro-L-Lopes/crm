namespace CRM.Domain.Repositories.Plan;
public interface IPlanReadOnlyRepository
{
    public Task<Entities.Plan> GetPlanById(Guid id);
    Task<bool> ExistsAndIsActiveAsync(Guid id);
}
