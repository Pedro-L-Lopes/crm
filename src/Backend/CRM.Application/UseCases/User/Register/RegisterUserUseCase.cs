using CRM.Application.Services;
using CRM.Application.Services.Validator;
using CRM.Application.UseCases.User.Register;
using CRM.Communication.Requests;
using CRM.Communication.Responses;
using CRM.Domain.Enums;
using CRM.Domain.Repositories;
using CRM.Domain.Repositories.Plan;
using CRM.Domain.Repositories.Tenant;
using CRM.Domain.Repositories.User;
using CRM.Domain.Security.Cryptography;
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
    private readonly IPlanReadOnlyRepository _readOnlyPlanRepository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessToken;
    private readonly IRoleValidator _roleValidator;

    public RegisterUserUseCase(
        IUserWriteOnlyRepository writeOnlyRepository,
        IUserReadOnlyRepository readOnlyRepository,
        IUnityOfWork unityOfWork,
        IPasswordEncripter passwordEncripter,
        ITenantReadOnlyRepository readOnlyTenantRepository,
        IAccessTokenGenerator accessToken,
        IPlanReadOnlyRepository readOnlyPlanRepository,
        IRoleValidator roleValidator)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _passwordEncripter = passwordEncripter;
        _unityOfWork = unityOfWork;
        _readOnlyTenantRepository = readOnlyTenantRepository;
        _accessToken = accessToken;
        _readOnlyPlanRepository = readOnlyPlanRepository;
        _roleValidator = roleValidator;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
        var (currentUserId, currentTenantId, currentRole) = await _roleValidator.GetUserContextAsync();

        var targetTenantId = DetermineTargetTenant(request.TenantId, currentTenantId, currentRole);

        await Validate(request, targetTenantId);

        var tenant = await _readOnlyTenantRepository.GetTenantById(targetTenantId);

        if (tenant == null)
            throw new CRMException(ResourceMessageException.INVALID_TENANT_OR_INACTIVE);

        if (tenant.PlanId == Guid.Empty)
            throw new CRMException(ResourceMessageException.INVALID_TENANT_OR_INACTIVE);

        var user = request.Adapt<Domain.Entities.User>();
        user.Password = _passwordEncripter.Encrypt(request.Password);
        user.Id = Guid.NewGuid();
        user.TenantId = targetTenantId;

        var plan = await _readOnlyPlanRepository.GetPlanById(tenant.PlanId);

        await _writeOnlyRepository.Add(user);
        await _unityOfWork.Commit();

        var token = _accessToken.Generate(user, tenant, plan);

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

    /// <summary>
    /// Determina qual tenant será usado:
    /// - SuperAdmin: pode escolher qualquer tenant (usa o do request)
    /// - Owner/Admin: sempre usa o próprio tenant (ignora o do request)
    /// </summary>
    private Guid DetermineTargetTenant(Guid requestTenantId, Guid currentTenantId, Role currentRole)
    {
        // SuperAdmin pode criar em qualquer tenant
        if (_roleValidator.CanModifyField(currentRole, "User", "TenantId"))
        {
            if (requestTenantId == Guid.Empty)
            {
                throw new CRMException(ResourceMessageException.INVALID_TENANT_OR_INACTIVE);
            }

            return requestTenantId;
        }

        return currentTenantId;
    }

    private async Task Validate(RequestRegisterUserJson request, Guid tenantIdToValidate)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var tenantExists = await _readOnlyTenantRepository.ExistActiveTenant(tenantIdToValidate);

        if (!tenantExists)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(
                nameof(request.TenantId),
                ResourceMessageException.INVALID_TENANT));
        }

        var emailExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(
                string.Empty,
                ResourceMessageException.EMAIL_ALREDY_REGISTERED));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}