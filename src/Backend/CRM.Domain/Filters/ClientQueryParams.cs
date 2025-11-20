using CRM.Domain.Common;

namespace CRM.Domain.Filters;

public class ClientQueryParams
{
    public PaginationParams Pagination { get; set; } = new();
    public ClientFilters Filters { get; set; } = new();
    public SortParams Sort { get; set; } = new();

    public ClientQueryParams() { }

    public ClientQueryParams(PaginationParams pagination, ClientFilters filters, SortParams sort)
    {
        Pagination = pagination;
        Filters = filters;
        Sort = sort;
    }
}
