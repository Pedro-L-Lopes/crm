using CRM.Communication.Responses;
using CRM.Domain.Repositories.Plan;
using CRM.Exceptions;
using CRM.Exceptions.ExceptionsBase;

namespace CRM.Application.UseCases.Plan.Get;
public class GetPlanByIdUseCase : IGetPlanByIdUseCase
{
    private readonly IPlanReadOnlyRepository _planReadOnlyRepository;

    public GetPlanByIdUseCase(IPlanReadOnlyRepository planReadOnlyRepository)
    {
        _planReadOnlyRepository = planReadOnlyRepository;
    }

    public async Task<ResponsePlanJson> Execute(Guid id)
    {
        var plan = await Validate(id);

        return MapPlanToResponse(plan);
    }

    private async Task<Domain.Entities.Plan> Validate(Guid id)
    {
        var plan = await _planReadOnlyRepository.GetPlanById(id);

        if (plan is null)
        {
            throw new ErrorOnValidationException(new List<string> { ResourceMessageException.PLAN_NOT_FOUND_OR_INACTIVE });
        }

        return plan;
    }

    private static ResponsePlanJson MapPlanToResponse(Domain.Entities.Plan plan)
    {
        return new ResponsePlanJson
        {
            Id = plan.Id,
            Name = plan.Name,
            Type = plan.Type,
            MonthlyPrice = plan.MonthlyPrice,
            AnnualPrice = plan.AnnualPrice,
            MaxUsers = plan.MaxUsers,
            MaxProperties = plan.MaxProperties,
            MaxStorageMb = plan.MaxStorageMb,
            CanExportData = plan.CanExportData,
            HasWhatsappAutomation = plan.HasWhatsappAutomation,
            HasDigitalSignature = plan.HasDigitalSignature,
            HasSupportPriority = plan.HasSupportPriority
        };
    }


}
