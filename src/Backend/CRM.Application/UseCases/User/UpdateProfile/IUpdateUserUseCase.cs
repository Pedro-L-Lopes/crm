using CRM.Communication.Requests;

namespace CRM.Application.UseCases.User.UpdateProfile;

public interface IUpdateUserUseCase
{
    public Task Execute(RequestUpdateUserProfileJson request);
}
