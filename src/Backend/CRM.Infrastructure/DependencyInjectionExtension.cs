using CRM.Domain.Repositories;
using CRM.Domain.Repositories.Plan;
using CRM.Domain.Repositories.Tenant;
using CRM.Domain.Repositories.User;
using CRM.Domain.Security.Tokens;
using CRM.Infrastructure.DataAccess;
using CRM.Infrastructure.DataAccess.Repositories;
using CRM.Infrastructure.Extensions;
using CRM.Infrastructure.Security.Access.Generator;
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
        AddDbContext(services, configuration);
        AddFluentMigrator_MySql(services, configuration);
        AddRepositories(services);
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

        services.AddScoped<ITenantWriteOnlyRepository, TenantRepository>();
        services.AddScoped<ITenantReadOnlyRepository, TenantRepository>();

        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();

        services.AddScoped<IPlanWriteOnlyRepository, PlanRepository>();
        services.AddScoped<IPlanReadOnlyRepository, PlanRepository>();
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

        services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signinKey));
    }
}
