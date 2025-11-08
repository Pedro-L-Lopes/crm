using CRM.Application.UseCases.Plan.Get;
using CRM.Communication.Requests;
using CRM.Communication.Responses;
using CRM.Domain.Enums;
using CRM.Domain.Repositories;
using CRM.Domain.Repositories.Tenant;
using CRM.Exceptions;
using CRM.Exceptions.ExceptionsBase;
using Mapster;

namespace CRM.Application.UseCases.Tenant.Register;
public class RegisterTenantUseCase : IRegisterTenantUseCase
{
    private readonly ITenantWriteOnlyRepository _writeOnlyRepository;
    private readonly ITenantReadOnlyRepository _readOnlyRepository;
    private readonly IGetPlanByIdUseCase _getPlanAndValidateUseCase;
    private readonly IUnityOfWork _unityOfWork;

    public RegisterTenantUseCase(ITenantWriteOnlyRepository writeOnlyRepository,
                                 ITenantReadOnlyRepository readOnlyRepository,
                                 IGetPlanByIdUseCase getPlanAndValidateUseCase,
                                 IUnityOfWork unityOfWork)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _getPlanAndValidateUseCase = getPlanAndValidateUseCase;
        _unityOfWork = unityOfWork;
    }

    public async Task<ResponseRegisterTenantJson> Execute(RequestRegisterTenantJson request)
    {
        await ValidateBusinessRules(request);

        var plan = await _getPlanAndValidateUseCase.Execute(request.PlanId);

        var tenant = request.Adapt<Domain.Entities.Tenant>();
        tenant.Id = Guid.NewGuid();
        tenant.PlanId = plan.Id;

        tenant.PlanExpiration = CalculateExpiryDate(plan.Type, request.Cycle);

        await _writeOnlyRepository.Add(tenant);
        await _unityOfWork.commit();

        return new ResponseRegisterTenantJson
        {
            Id = tenant.Id,
            Name = request.Name,
        };
    }

    /// <summary>
    /// Calcula a data de expiração com base no tipo do plano e no ciclo de pagamento.
    /// </summary>
    private DateTime? CalculateExpiryDate(PlanType type, BillingCycle cycle)
    {
        if (type == PlanType.Trial)
        {
            return DateTime.UtcNow.AddDays(15 + 1);
        }

        // 0
        if (cycle == BillingCycle.monthly)
        {
            return DateTime.UtcNow.AddMonths(1).AddDays(1);
        }

        // 1
        if (cycle == BillingCycle.annual)
        {
            return DateTime.UtcNow.AddYears(1).AddDays(1);
        }

        return null;
    }

    /// <summary>
    /// Valida o formato do request (FluentValidation) e regras de negócio (ex: email).
    /// </summary>
    private async Task ValidateBusinessRules(RequestRegisterTenantJson request)
    {
        var validator = new RegisterTenantValidator();
        var result = validator.Validate(request);
        var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

        // Validação de E-mail
        var emailExist = await _readOnlyRepository.ExistActiveTenantWithEmail(request.Email);
        if (emailExist)
        {
            errorMessages.Add(ResourceMessageException.EMAIL_ALREDY_REGISTERED);
        }

        if (errorMessages.Any())
        {
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}