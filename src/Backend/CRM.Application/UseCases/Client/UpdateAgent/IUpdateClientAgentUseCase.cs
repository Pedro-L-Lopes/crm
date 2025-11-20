using CRM.Communication.Requests.Client;

namespace CRM.Application.UseCases.Client.UpdateAgent;

public interface IUpdateClientAgentUseCase
{
    public Task Execute(RequestClientUpdateAgentJson request, string clientId);
}
