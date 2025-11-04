using CRM.Application.Services.Cryptography;
using CRM.Application.UseCases.User.Register;
using CRM.Communication.Requests;
using CRM.Domain.Repositories;
using CRM.Domain.Repositories.Tenant;
using CRM.Domain.Repositories.User;
using CRM.Exceptions;
using CRM.Exceptions.ExceptionsBase;
using Mapster;

namespace CRM.Application.UseCases.Tenant.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly ITenantReadOnlyRepository _readOnlyTenantRepository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly PasswordEncripter _passwordEncripter;

    public RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository, 
                               IUserReadOnlyRepository readOnlyRepository,
                               IUnityOfWork unityOfWork, 
                               PasswordEncripter passwordEncripter,
                               ITenantReadOnlyRepository readOnlyTenantRepository)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _passwordEncripter = passwordEncripter;
        _unityOfWork = unityOfWork;
        _readOnlyTenantRepository = readOnlyTenantRepository;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {

        await Validate(request);

        var user = request.Adapt<Domain.Entities.User>();

        user.Password = _passwordEncripter.Encrypt(request.Password);

        await _writeOnlyRepository.Add(user);

        await _unityOfWork.commit();

        return new ResponseRegisterUserJson
        {
            Name = request.Name,
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var tenantExists = await _readOnlyTenantRepository.ExistActiveTenant(request.TenantId);
        if (!tenantExists)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.TenantId), ResourceMessageException.INVALID_TENANT));
        }

        var emailExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);
        if (emailExist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessageException.EMAIL_ALREDY_REGISTERED));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
