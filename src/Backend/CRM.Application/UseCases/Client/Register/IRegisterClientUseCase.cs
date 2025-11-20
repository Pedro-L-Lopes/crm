using CRM.Communication.Requests;
using CRM.Communication.Requests.Client;
using CRM.Communication.Responses;
using CRM.Communication.Responses.Client;

namespace CRM.Application.UseCases.Client.Register;

public interface IRegisterClientUseCase
{
    Task<ResponseClientJson> Execute(RequestClientJson request);
}
