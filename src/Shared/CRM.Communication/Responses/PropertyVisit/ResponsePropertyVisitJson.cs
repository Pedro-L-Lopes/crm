using CRM.Communication.Responses.Client;
using CRM.Communication.Responses.Property;

namespace CRM.Communication.Responses.PropertyVisit;

public class ResponsePropertyVisitJson
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Guid TenantId { get; set; }
    public Guid PropertyId { get; set; }
    public Guid ClientId { get; set; }
    public Guid AgentId { get; set; }

    public DateTime ScheduledAt { get; set; }
    public string Status { get; set; } = null!;
    public string? Notes { get; set; }
    public bool SummaryCopied { get; set; }

    public ResponsePropertyJson? Property { get; set; }
    public ResponseClientJson? Client { get; set; }
}
