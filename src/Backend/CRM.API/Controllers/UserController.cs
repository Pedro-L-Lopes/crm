using CRM.Application.UseCases.User.Register;
using CRM.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

public class UserController : CRMBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromBody]RequestRegisterUserJson request, [FromServices] IRegisterUserUseCase useCase)
    {
       
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}
