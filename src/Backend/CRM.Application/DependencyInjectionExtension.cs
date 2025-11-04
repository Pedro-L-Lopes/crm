using CRM.Application.Services.Cryptography;
using CRM.Application.Services.Mappings;
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
        services.AddScoped<IRegisterTenantUseCase, RegisterTenantUseCase>();

        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }

    private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
    {
        var keyAdditional = configuration.GetValue<string>("Settings:Password:AdditionalKey");

        services.AddScoped(option => new PasswordEncripter(keyAdditional!));
    }
}
