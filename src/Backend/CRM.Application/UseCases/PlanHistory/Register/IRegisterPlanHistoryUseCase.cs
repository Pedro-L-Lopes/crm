using CRM.Communication.Requests;
using CRM.Communication.Responses;

namespace CRM.Application.UseCases.PlanHistory.Register;
public interface IRegisterPlanHistoryUseCase
{
    Task<ResponsePlanHistoryJson> Execute(RequestRegisterPlanHistoryJson  request);
}
