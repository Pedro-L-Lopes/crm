using Azure;
using CRM.Application.UseCases.Plan.Get;
using CRM.Application.UseCases.Plan.Register;
using CRM.Communication.Requests;
using CRM.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

public class PlanController : CRMBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterPlanJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseRegisterPlanJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterPlanUseCase useCase,
        [FromBody] RequestRegisterPlanJson request)
    {
        var response = await useCase.Execute(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ResponsePlanJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromServices] IGetPlanByIdUseCase useCase,
        [FromRoute] Guid id)
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }
}
