using CRM.Application.UseCases.Tenant.Register;
using CRM.Communication.Requests;
using CRM.Communication.Responses;
using Microsoft.AspNetCore.Mvc;


[Route("/[controller]")]
[ApiController]
public class TenantController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterTenantJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromBody]RequestRegisterTenantJson request, [FromServices] IRegisterTenantUseCase useCase)
    {

        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}
