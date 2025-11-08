using CRM.Domain.Repositories.Plan;

namespace CRM.Application.UseCases.Plan.Validate;

public class ValidatePlanUseCase : IValidatePlanUseCase
{
    private readonly IPlanReadOnlyRepository _repository;

    public ValidatePlanUseCase(IPlanReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Execute(Guid planId)
    {
        return await _repository.ExistsAndIsActiveAsync(planId);
    }
}