using CRM.Domain.Enums;

namespace CRM.Communication.Requests.Client;

public class RequestClientJson
{
    public Guid TenantId { get; set; }
    public Guid AgentId { get; set; }
    public Guid? AddressId { get; set; }

    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }

    public string Document { get; set; } = null!;
    public string? SecondDocument { get; set; }

    public string? Type { get; set; }
    public string? Notes { get; set; }

    public DateTime? BirthDate { get; set; }
    public string? Occupation { get; set; }
    public decimal? Income { get; set; }
    public string? Gender { get; set; }
}
