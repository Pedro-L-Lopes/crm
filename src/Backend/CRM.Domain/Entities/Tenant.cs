namespace CRM.Domain.Entities;
public class Tenant : EntityBase
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SubDomain { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public Guid PlanId { get; set; }
    public DateTime PlanExpiration { get; set; }
}
