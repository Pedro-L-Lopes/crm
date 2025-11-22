using CRM.Domain.Common;
using CRM.Domain.Filters;

namespace CRM.Domain.Repositories.Client;

public interface IClientReadOnlyRepository
{
    Task<bool> ExistsClientWhiteDocument(string document);
    Task<PagedResult<Entities.Client>> GetPaged(ClientQueryParams queryParams);
    Task<Entities.Client?> GetClient(Entities.User user, Guid clientId);
}
