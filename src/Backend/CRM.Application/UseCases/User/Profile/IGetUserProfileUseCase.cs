using CRM.Communication.Responses;

namespace CRM.Application.UseCases.User.Profile;
public interface IGetUserProfileUseCase
{
    public Task<ResponseUserProfileJson> Execute();
}
