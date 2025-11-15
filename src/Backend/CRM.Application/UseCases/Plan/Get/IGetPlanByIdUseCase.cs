using CRM.Communication.Responses;

namespace CRM.Application.UseCases.Plan.Get;
public interface IGetPlanByIdUseCase
{
    Task<ResponsePlanJson> Execute(Guid id);
}
