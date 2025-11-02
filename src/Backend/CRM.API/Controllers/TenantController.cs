using CRM.Communication.Requests;
using CRM.Communication.Responses;
using Microsoft.AspNetCore.Mvc;


[Route("/[controller]")]
[ApiController]
public class TenantController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterTenantJson), StatusCodes.Status201Created)]
    public IActionResult Register(RequestRegisterTenantJson request)
    {
        return Created();
    }
}
