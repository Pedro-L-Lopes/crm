using CRM.Communication.Requests;
using CRM.Communication.Responses;

namespace CRM.Application.UseCases.Tenant.Register;
public interface IRegisterTenantUseCase
{
    public Task<ResponseRegisterTenantJson> Execute(RequestRegisterTenantJson request);
}
