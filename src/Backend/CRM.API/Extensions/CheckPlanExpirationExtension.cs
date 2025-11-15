namespace CRM.API.Extensions;

public static class CheckPlanExpirationExtension
{
    public static IApplicationBuilder UsePlanExpirationCheck(this IApplicationBuilder app)
    {
        return app.UseMiddleware<Middleware.CheckPlanExpirationMiddleware>();
    }
}
