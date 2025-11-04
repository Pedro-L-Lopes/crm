using CRM.Domain.Repositories;
using CRM.Domain.Repositories.Tenant;
using CRM.Domain.Repositories.User;
using CRM.Infrastructure.DataAccess;
using CRM.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("mysql");
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
    }
}
