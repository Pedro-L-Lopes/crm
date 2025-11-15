namespace CRM.Application.UseCases.Plan.Validate;

public interface IValidatePlanUseCase
{
    Task<bool> Execute(Guid planId);
}