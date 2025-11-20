namespace CRM.Domain.Filters;

public class ClientFilters
{
    public Guid? TenantId { get; set; }
    public Guid? AgentId { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Document { get; set; }
    public string? Type { get; set; }
    public string? Gender { get; set; }
}
