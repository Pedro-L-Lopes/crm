using CRM.Application.Services.Cryptography;
using CRM.Application.Services.Mappings;
using CRM.Application.UseCases.Login.DoLogin;
using CRM.Application.UseCases.Plan.Get;
using CRM.Application.UseCases.Plan.Register;
using CRM.Application.UseCases.Plan.Validate;
using CRM.Application.UseCases.Tenant.Register;
using CRM.Application.UseCases.User.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddPasswordEncrypter(services, configuration);
        AddMapperConfigurations();
        AddUseCases(services);
        //AddTokenService(services);
    }

    public  static void AddMapperConfigurations()
    {
        MapConfigurations.Configure();
    }

    private static void AddUseCases(IServiceCollection services)
    {
        // AUTH
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        
        // TENANT
        services.AddScoped<IRegisterTenantUseCase, RegisterTenantUseCase>();
        
        // PLAN
        services.AddScoped<IRegisterPlanUseCase, RegisterPlanUseCase>();
        services.AddScoped<IValidatePlanUseCase, ValidatePlanUseCase>();
        services.AddScoped<IGetPlanByIdUseCase, GetPlanByIdUseCase>();


    }

    private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
    {
        var keyAdditional = configuration.GetValue<string>("Settings:Password:AdditionalKey");

        services.AddScoped(option => new PasswordEncripter(keyAdditional!));
    }
}
