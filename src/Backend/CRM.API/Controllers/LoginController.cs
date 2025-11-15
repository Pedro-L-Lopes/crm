using Azure;
using CRM.Application.UseCases.Login.DoLogin;
using CRM.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

public class LoginController : CRMBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
