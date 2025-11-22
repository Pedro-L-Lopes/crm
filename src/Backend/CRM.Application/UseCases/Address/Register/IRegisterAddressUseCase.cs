using CRM.Communication.Requests.Address;
using CRM.Communication.Responses.Address;

namespace CRM.Application.UseCases.Address.Register;

public interface IRegisterAddressUseCase
{
    Task<ResponseAddressJson> Execute(RequestAddressJson request);
}
