namespace CRM.Domain.Repositories.Client;

public interface IClientWriteOnlyRepository
{
    public Task Add(Entities.Client client);
    Task Delete(Guid id);
}
