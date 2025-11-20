using CRM.Domain.Repositories;
using CRM.Domain.Repositories.Client;
using CRM.Domain.Repositories.Plan;
using CRM.Domain.Repositories.PlanHistory;
using CRM.Domain.Repositories.Tenant;
using CRM.Domain.Repositories.User;
using CRM.Domain.Security.Cryptography;
using CRM.Domain.Security.Tokens;
using CRM.Domain.Services.LoggedUser;
using CRM.Infrastructure.DataAccess;
using CRM.Infrastructure.DataAccess.Repositories;
using CRM.Infrastructure.Extensions;
using CRM.Infrastructure.Security.Access.Generator;
using CRM.Infrastructure.Security.Access.Validator;
using CRM.Infrastructure.Security.Cryptography;
using CRM.Infrastructure.Services.LoggedUser;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CRM.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddPasswordEncrypter(services, configuration);
        AddDbContext(services, configuration);
        AddFluentMigrator_MySql(services, configuration);
        AddRepositories(services);
        AddLoggedUser(services);
        AddTokens(services, configuration);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<CRMDbContext>(DbContextOptions =>
        {
            DbContextOptions.UseMySql(connectionString, serverVersion);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnityOfWork, UnityOfWork>();

        // TENANT
        services.AddScoped<ITenantWriteOnlyRepository, TenantRepository>();
        services.AddScoped<ITenantReadOnlyRepository, TenantRepository>();

        // USER
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();

        // PLAN
        services.AddScoped<IPlanWriteOnlyRepository, PlanRepository>();
        services.AddScoped<IPlanReadOnlyRepository, PlanRepository>();

        // PLAN HISTORY
        services.AddScoped<IPlanHistoryReadOnlyRepository, PlanHistoryRepository>();
        services.AddScoped<IPlanHistoryWriteOnlyRepository, PlanHistoryRepository>();

        // CLIENT
        services.AddScoped<IClientReadOnlyRepository, ClientRepository>();
        services.AddScoped<IClientWriteOnlyRepository, ClientRepository>();
        services.AddScoped<IClientUpdateOnlyRepository, ClientRepository>();
    }

    private static void AddFluentMigrator_MySql(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options.AddMySql5()
            .WithGlobalConnectionString(configuration.ConnectionString())
            .ScanIn(Assembly.Load("CRM.Infrastructure")).For.All();
        });
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
        var signinKey = configuration.GetValue<string>("Settings:Jwt:SigninKey");   

        services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signinKey!));
        services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signinKey!));
    }

    private static void AddLoggedUser(IServiceCollection services)
    {
        services.AddScoped<ILoggedUser, LoggedUser>();
    }

    private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
    {
        var keyAdditional = configuration.GetValue<string>("Settings:Password:AdditionalKey");

        services.AddScoped<IPasswordEncripter>(option => new BCryptNet(keyAdditional!));
    }
}
