namespace CRM.Communication.Requests.Client;

public class RequestClientQuery : RequestPaginationQuery
{
    public string? TenantId { get; set; }
    public string? AgentId { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Document { get; set; }
    public string? Type { get; set; }
    public string? Gender { get; set; }

    public string? OrderBy { get; set; }
    public bool Descending { get; set; } = false;
}
