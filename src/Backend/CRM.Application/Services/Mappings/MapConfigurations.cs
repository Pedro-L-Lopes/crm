using CRM.Communication.Requests;
using CRM.Communication.Requests.Client;
using CRM.Communication.Responses;
using CRM.Communication.Responses.Client;
using CRM.Domain.Common;
using CRM.Domain.Entities;
using CRM.Domain.Filters;
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

        config.NewConfig<Client, ResponseClientJson>()
               .Map(dest => dest.Address, src => src.Address);

        config.NewConfig<RequestClientQuery, ClientQueryParams>()
            .Map(dest => dest.Pagination, src => new PaginationParams(src.Page, src.PageSize))
            .Map(dest => dest.Filters, src => src)
            .Map(dest => dest.Sort, src => src);

        config.NewConfig<Client, ResponseShortClientJson>();

        config.NewConfig<PagedResult<Client>, ResponsePagedListJson<ResponseShortClientJson>>()
            .Map(dest => dest.Items, src => src.Items);
    }

}
