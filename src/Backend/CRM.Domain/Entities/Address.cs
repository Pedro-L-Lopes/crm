namespace CRM.Domain.Entities;

public class Address : EntityBase
{
    public Guid TenantId { get; set; }

    public string? ZipCode { get; set; }
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? Complement { get; set; }
    public string? Neighborhood { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }

    public ICollection<Client>? Clients { get; set; }
    public ICollection<Property>? Properties { get; set; }
}

