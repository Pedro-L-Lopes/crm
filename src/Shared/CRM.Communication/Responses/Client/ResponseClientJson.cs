using CRM.Communication.Responses.Address;
using CRM.Domain.Enums;

namespace CRM.Communication.Responses.Client;

public class ResponseClientJson
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Guid TenantId { get; set; }
    public Guid AgentId { get; set; }
    public Guid? AddressId { get; set; }

    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }

    public string Document { get; set; } = null!;
    public string? SecondDocument { get; set; }

    public ClientType Type { get; set; }
    public string? Notes { get; set; }

    public DateTime? BirthDate { get; set; }
    public string? Occupation { get; set; }
    public decimal? Income { get; set; }
    public string? Gender { get; set; }

    public ResponseAddressJson? Address { get; set; }
}
