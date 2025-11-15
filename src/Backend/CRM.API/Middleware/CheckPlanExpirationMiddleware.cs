using CRM.Exceptions.ExceptionsBase;

namespace CRM.API.Middleware;

public class CheckPlanExpirationMiddleware
{
    private readonly RequestDelegate _next;

    public CheckPlanExpirationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var expirationClaim = context.User.FindFirst("plan_expiration");

            if (expirationClaim is not null && !string.IsNullOrEmpty(expirationClaim.Value))
            {
                if (DateTime.TryParse(expirationClaim.Value, out DateTime expirationDate))
                {
                    if (DateTime.UtcNow > expirationDate)
                    {
                        throw new PlanExpiredException();
                    }
                }
            }
        }

        await _next(context);
    }
}