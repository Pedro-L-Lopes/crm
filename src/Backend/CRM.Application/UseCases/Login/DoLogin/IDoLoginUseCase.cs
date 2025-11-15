using CRM.Communication.Requests;

namespace CRM.Application.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    public Task<ResponseRegisterUserJson> Execute(RequestLoginJson request);
}
