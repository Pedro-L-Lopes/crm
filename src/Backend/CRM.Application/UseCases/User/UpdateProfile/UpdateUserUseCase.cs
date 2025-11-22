using CRM.Communication.Requests;
using CRM.Domain.Repositories;
using CRM.Domain.Repositories.User;
using CRM.Domain.Services.LoggedUser;
using CRM.Exceptions;
using CRM.Exceptions.ExceptionsBase;

namespace CRM.Application.UseCases.User.UpdateProfile;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnityOfWork _unityOfWork;

    public UpdateUserUseCase(
        ILoggedUser loggedUser,
        IUserUpdateOnlyRepository repository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnityOfWork unityOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unityOfWork = unityOfWork;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task Execute(RequestUpdateUserProfileJson request)
    {
        var loggedUser = await _loggedUser.User();

        await Validate(request, loggedUser.Email);

        var user = await _repository.GetById(loggedUser.Id);

        user.Name = request.Name;
        user.Email = request.Email;
        user.Phone = request.Phone;
        user.Gender = request.Gender;

        _repository.Update(user);

        await _unityOfWork.Commit();
    }

    private async Task Validate(RequestUpdateUserProfileJson request, string currentEmail)
    {
        var validator = new UpdateUserUseValidator();

        var result = await validator.ValidateAsync(request);

        if (currentEmail.Equals(request.Email) == false)
        {
            var userExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
            if (userExist)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessageException.EMAIL_ALREDY_REGISTERED));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }

}
