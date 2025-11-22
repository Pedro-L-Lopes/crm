using CRM.Communication.Requests;
using CRM.Communication.Requests.Client;

namespace CRM.Application.UseCases.Client.UpdateClient;

public interface IUpdateClientUseCase
{
    public Task Execute(RequestClientJson request, Guid clientId);
}
