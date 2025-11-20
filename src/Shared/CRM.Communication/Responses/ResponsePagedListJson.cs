using CRM.Domain.Common;

namespace CRM.Communication.Responses;

public class ResponsePagedListJson<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public IEnumerable<T> Items { get; set; } = new List<T>();

    public static ResponsePagedListJson<T> FromPagedResult(PagedResult<T> pagedResult)
    {
        return new ResponsePagedListJson<T>
        {
            Page = pagedResult.Page,
            PageSize = pagedResult.PageSize,
            TotalItems = pagedResult.TotalItems,
            TotalPages = pagedResult.TotalPages,
            HasPreviousPage = pagedResult.HasPreviousPage,
            HasNextPage = pagedResult.HasNextPage,
            Items = pagedResult.Items
        };
    }
}
