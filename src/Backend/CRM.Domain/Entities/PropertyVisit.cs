namespace CRM.Domain.Entities;

public class PropertyVisit : EntityBase
{
    public Guid TenantId { get; set; }
    public Guid PropertyId { get; set; }
    public Guid ClientId { get; set; }
    public Guid AgentId { get; set; }

    public DateTime ScheduledAt { get; set; }
    public string? Status { get; set; }
    public string? Notes { get; set; }
    public bool SummaryCopied { get; set; }

    public Property Property { get; set; } = null!;
    public Client Client { get; set; } = null!;
}
