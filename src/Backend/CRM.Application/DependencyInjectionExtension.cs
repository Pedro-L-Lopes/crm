using CRM.Application.Services.Mappings;
using CRM.Application.Services.Validator;
using CRM.Application.UseCases.Client.Delete;
using CRM.Application.UseCases.Client.GetClient;
using CRM.Application.UseCases.Client.GetClientsPaged;
using CRM.Application.UseCases.Client.Register;
using CRM.Application.UseCases.Client.SoftDelete;
using CRM.Application.UseCases.Client.UpdateAgent;
using CRM.Application.UseCases.Client.UpdateClient;
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
        AddServices(services);
        //AddTokenService(services);
    }

    public static void AddMapperConfigurations(IServiceCollection services)
    {
        MapConfigurations.Configure();
        var config = TypeAdapterConfig.GlobalSettings;
        services.AddSingleton(config);
        services.AddScoped<IMapper, Mapper>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IRoleValidator, RoleValidator>();
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

        // CLIENT
        services.AddScoped<IRegisterClientUseCase, RegisterClientUseCase>();
        services.AddScoped<IGetClientsPagedUseCase, GetClientsPagedUseCase>();
        services.AddScoped<IGetClientsUseCase, GetClientsUseCase>();
        services.AddScoped<IUpdateClientUseCase, UpdateClientUseCase>();
        services.AddScoped<IDeleteClientUseCase, DeleteClientUseCase>();
        services.AddScoped<ISoftDeleteClientUseCase, SoftDeleteClientUseCase>();
        services.AddScoped<IUpdateClientAgentUseCase, UpdateClientAgentUseCase>();

        // ADDRESS


    }

    
}
