using CRM.Application.Services.Cryptography;
using CRM.Application.UseCases.User.Register;
using CRM.Communication.Requests;
using CRM.Communication.Responses;
using CRM.Domain.Entities;
using CRM.Domain.Repositories;
using CRM.Domain.Repositories.Tenant;
using CRM.Domain.Repositories.User;
using CRM.Domain.Security.Tokens;
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
    private readonly IAccessTokenGenerator _accessToken;

    public RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository, 
                               IUserReadOnlyRepository readOnlyRepository,
                               IUnityOfWork unityOfWork, 
                               PasswordEncripter passwordEncripter,
                               ITenantReadOnlyRepository readOnlyTenantRepository,
                               IAccessTokenGenerator accessToken)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _passwordEncripter = passwordEncripter;
        _unityOfWork = unityOfWork;
        _readOnlyTenantRepository = readOnlyTenantRepository;
        _accessToken = accessToken;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {

        await Validate(request);

        var tenant = await _readOnlyTenantRepository.GetTenantById(request.TenantId);

        var user = request.Adapt<Domain.Entities.User>();
        user.Password = _passwordEncripter.Encrypt(request.Password);
        user.Id = Guid.NewGuid();

        await _writeOnlyRepository.Add(user);
        await _unityOfWork.commit();

        var token = _accessToken.Generate(user, tenant);

        return new ResponseRegisterUserJson
        {
            Name = request.Name,
            Email = user.Email,
            TenantName = tenant.Name,
            TenantType = tenant.Type,
            PlanExpiration = tenant.PlanExpiration,
            Tokens = new ReponseTokenJson
            {
                AccessToken = token
            }
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
