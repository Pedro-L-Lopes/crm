using CRM.Communication.Requests;
using CRM.Communication.Responses;

namespace CRM.Application.UseCases.Plan.Register;
public interface IRegisterPlanUseCase
{
    Task<ResponseRegisterPlanJson> Execute(RequestRegisterPlanJson request);
}