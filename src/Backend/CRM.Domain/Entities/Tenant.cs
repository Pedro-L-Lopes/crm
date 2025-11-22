using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Domain.Entities;
public class Tenant : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string SubDomain { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public string? EntityType { get; set; } = string.Empty;
    public string? Document { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public Guid PlanId { get; set; }
    public BillingCycle Cycle { get; set; }
    public DateTime? PlanExpiration { get; set; }


    public Guid? CurrentPlanHistoryId { get; set; }


    [ForeignKey("PlanId")]
    public Plan? Plan { get; set; }

    [ForeignKey("CurrentPlanHistoryId")]
    public PlanHistory? CurrentPlanHistory { get; set; }
}
