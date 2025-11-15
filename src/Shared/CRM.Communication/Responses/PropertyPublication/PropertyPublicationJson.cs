using CRM.Communication.Responses.Property;

namespace CRM.Communication.Responses.PropertyPublication;

internal class PropertyPublicationJson
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }
    public Guid PropertyId { get; set; }

    public string Platform { get; set; } = null!;
    public string Link { get; set; } = null!;
    public string? Caption { get; set; }
    public DateTime? PostDate { get; set; }

    public ResponsePropertyJson? Property { get; set; }
}
