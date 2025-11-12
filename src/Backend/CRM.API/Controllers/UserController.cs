using CRM.API.Attributes;
using CRM.Application.UseCases.User.ChangePassword;
using CRM.Application.UseCases.User.Profile;
using CRM.Application.UseCases.User.Register;
using CRM.Application.UseCases.User.UpdateProfile;
using CRM.Communication.Requests;
using CRM.Communication.Responses;
using CRM.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

public class UserController : CRMBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    [AuthorizedByRole(Role.owner, Role.admin)]
    public async Task<IActionResult> Register([FromBody]RequestRegisterUserJson request, [FromServices] IRegisterUserUseCase useCase)
    {
       
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
    [AuthenticatedUser]
    public async Task<IActionResult> GetUserProfile([FromServices] IGetUserProfileUseCase useCase)
    {
        var result = await useCase.Execute();

        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateUserUseCase useCase,
        [FromBody] RequestUpdateUserProfileJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }

    [HttpPut("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> ChangePassword(
        [FromServices] IChangePasswordUseCase useCase,
        [FromBody] RequestChangePasswordJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }

    //[HttpDelete]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[AuthenticatedUser]
    //public async Task<IActionResult> Delete([FromServices] IRequestDeleteUserUseCase useCase)
    //{
    //    await useCase.Execute();

    //    return NoContent();
    //}
}
