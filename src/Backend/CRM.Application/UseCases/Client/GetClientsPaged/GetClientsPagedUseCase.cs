using CRM.Communication.Requests;
using CRM.Communication.Requests.Client;
using CRM.Communication.Responses;
using CRM.Communication.Responses.Client;
using CRM.Domain.Common;
using CRM.Domain.Filters;
using CRM.Domain.Repositories.Client;
using Mapster;

namespace CRM.Application.UseCases.Client.GetClientsPaged;

public class GetClientsPagedUseCase : IGetClientsPagedUseCase
{
    private readonly IClientReadOnlyRepository _repository;

    public GetClientsPagedUseCase(IClientReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponsePagedListJson<ResponseShortClientJson>> Execute(RequestClientQuery request)
    {
        var queryParams = request.Adapt<ClientQueryParams>();

        var pagedResult = await _repository.GetPaged(queryParams);

        var response = pagedResult.Adapt<ResponsePagedListJson<ResponseShortClientJson>>();

        return response;
    }
}
