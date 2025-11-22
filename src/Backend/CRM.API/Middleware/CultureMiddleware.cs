using System.Globalization;

namespace CRM.API.Middleware;
public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var surpportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);

        var requestCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var cultureInfo = new CultureInfo("en");

        if (string.IsNullOrWhiteSpace(requestCulture) == false && surpportedLanguages.Any(c => c.Name == requestCulture))
        {
            cultureInfo = new CultureInfo(requestCulture);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = new CultureInfo(requestCulture);

        await _next(context);
    }
}
