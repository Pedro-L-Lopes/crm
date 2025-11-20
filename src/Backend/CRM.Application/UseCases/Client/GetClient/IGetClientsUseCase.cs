using CRM.Communication.Responses.Client;

namespace CRM.Application.UseCases.Client.GetClient;

public interface IGetClientsUseCase
{
    Task<ResponseClientJson> Execute(Guid id);
}
