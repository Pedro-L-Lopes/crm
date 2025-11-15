using CRM.Application.Services.Mappings;
using CRM.Application.UseCases.Login.DoLogin;
using CRM.Application.UseCases.Plan.Get;
using CRM.Application.UseCases.Plan.Register;
using CRM.Application.UseCases.Plan.Validate;
using CRM.Application.UseCases.PlanHistory.Register;
using CRM.Application.UseCases.Tenant.Register;
using CRM.Application.UseCases.User.ChangePassword;
using CRM.Application.UseCases.User.Profile;
using CRM.Application.UseCases.User.Register;
using CRM.Application.UseCases.User.UpdateProfile;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddMapperConfigurations(services);
        AddUseCases(services);
        //AddTokenService(services);
    }

    public static void AddMapperConfigurations(IServiceCollection services)
    {
        MapConfigurations.Configure();
        var config = TypeAdapterConfig.GlobalSettings;
        services.AddSingleton(config);
        services.AddScoped<IMapper, Mapper>();
    }

    private static void AddUseCases(IServiceCollection services)
    {
        // AUTH
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        
        // TENANT
        services.AddScoped<IRegisterTenantUseCase, RegisterTenantUseCase>();

        // USER
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        
        // PLAN
        services.AddScoped<IRegisterPlanUseCase, RegisterPlanUseCase>();
        services.AddScoped<IValidatePlanUseCase, ValidatePlanUseCase>();
        services.AddScoped<IGetPlanByIdUseCase, GetPlanByIdUseCase>();

        // PLAN HISTORY
        services.AddScoped<IRegisterPlanHistoryUseCase, RegisterPlanHistoryUseCase>();


    }

    
}
