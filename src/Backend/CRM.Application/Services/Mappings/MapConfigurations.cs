using CRM.Communication.Requests;
using CRM.Communication.Responses;
using CRM.Domain.Entities;
using Mapster;

namespace CRM.Application.Services.Mappings;
public static class MapConfigurations
{
    public static void Configure()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        TypeAdapterConfig<RequestRegisterUserJson, Domain.Entities.User>
            .NewConfig()
            .Ignore(dest => dest.Password);

        config.NewConfig<User, ResponseUserProfileJson>()
                .Map(dest => dest.Tenant, src => src.Tenant)
                .Map(dest => dest.Plan, src => src.Tenant!.Plan);

        config.NewConfig<Tenant, ResponseUserProfileJson.TenantInfo>();

        config.NewConfig<Plan, ResponseUserProfileJson.PlanInfo>();

        config.NewConfig<PlanHistory, ResponsePlanHistoryJson>()
            .Map(dest => dest.Plan, src => src.Plan);

        config.NewConfig<Plan, ResponsePlanHistoryJson.PlanInfo>();
    }

}
