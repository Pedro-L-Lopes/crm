using CRM.Domain.Enums;
using System.Net;

namespace CRM.Domain.Entities;

public class Property : EntityBase
{
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

    public Client? Owner { get; set; }
    public Address? Address { get; set; }

    public ICollection<PropertyPublication>? Publications { get; set; }
    public ICollection<PropertyVisit>? Visits { get; set; }
}
