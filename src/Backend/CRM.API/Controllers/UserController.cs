using CRM.Application.UseCases.Tenant.Register;
using CRM.Application.UseCases.User.Register;
using CRM.Communication.Requests;
using CRM.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[Route("/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromBody]RequestRegisterUserJson request, [FromServices] IRegisterUserUseCase useCase)
    {
       
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}
