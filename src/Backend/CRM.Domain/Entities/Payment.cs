using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Domain.Entities;

public class Payment : EntityBase
{
    public Guid PlanHistoryId { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public string Method { get; set; } = "Manual"; // Pix, Card, etc.
    public string Status { get; set; } = "Paid"; // Paid, Pending, Failed
    public string? GatewayTransactionId { get; set; }

    [ForeignKey("PlanHistoryId")]
    public PlanHistory? PlanHistory { get; set; }
}
