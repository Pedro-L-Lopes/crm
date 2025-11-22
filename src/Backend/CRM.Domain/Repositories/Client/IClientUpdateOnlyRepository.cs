namespace CRM.Domain.Repositories.Client;

public interface IClientUpdateOnlyRepository
{
    Task<Entities.Client?> GetClient(Entities.User user, Guid clientId);
    public void Update(Entities.Client client);
    Task SoftDelete(Guid clientId);
    public void UpdateAgent(Entities.Client client, Guid agentId);
}
