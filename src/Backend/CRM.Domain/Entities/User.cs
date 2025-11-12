using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Domain.Entities;

public class User : EntityBase
{
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime LastLogin { get; set; }
    public bool IsActive { get; set; } = false;
    public bool TwoFactorEnabled { get; set; } = false;
    public string? Gender { get; set; }

    [ForeignKey("TenantId")]
    public Tenant? Tenant { get; set; }
}
