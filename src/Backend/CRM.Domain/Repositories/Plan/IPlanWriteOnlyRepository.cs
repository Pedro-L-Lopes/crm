namespace CRM.Domain.Repositories.Plan;
public interface IPlanWriteOnlyRepository
{
    public Task Add(Entities.Plan plan);
}
