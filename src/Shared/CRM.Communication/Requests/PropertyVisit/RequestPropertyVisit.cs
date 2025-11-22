namespace CRM.Communication.Requests.PropertyVisit;

public class RequestPropertyVisit
{
    public Guid TenantId { get; set; }
    public Guid PropertyId { get; set; }
    public Guid ClientId { get; set; }
    public Guid AgentId { get; set; }

    public DateTime ScheduledAt { get; set; }
    public string Status { get; set; } = "scheduled";
    public string? Notes { get; set; }

    public bool SummaryCopied { get; set; }
}
