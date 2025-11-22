using CRM.Communication.Responses.Address;
using CRM.Communication.Responses.Client;
using CRM.Domain.Enums;

namespace CRM.Communication.Responses.Property;

public class ResponsePropertyJson
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Guid TenantId { get; set; }
    public Guid? OwnerId { get; set; }
    public Guid? AddressId { get; set; }

    public int Code { get; set; }
    public string? Description { get; set; }
    public PropertyType PropertyType { get; set; }
    public PropertyPurpose Purpose { get; set; }
    public PropertyStatus Status { get; set; }
    public bool CanBeFinanced { get; set; }

    public decimal? Price { get; set; }

    public decimal? TotalArea { get; set; }
    public decimal? BuiltArea { get; set; }

    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int Suites { get; set; }
    public int ParkingSpaces { get; set; }
    public int? YearBuilt { get; set; }

    public ResponseClientJson? Owner { get; set; }
    public ResponseAddressJson? Address { get; set; }
}
