using CRM.Communication.Requests;
using CRM.Communication.Requests.Client;
using CRM.Communication.Responses;
using CRM.Communication.Responses.Client;

namespace CRM.Application.UseCases.Client.GetClientsPaged;
public interface IGetClientsPagedUseCase
{
    public Task<ResponsePagedListJson<ResponseShortClientJson>> Execute(RequestClientQuery request);
}
