namespace CRM.Communication.Requests.PropertyPublication;

public class RequestPropertyPublication
{
    public Guid TenantId { get; set; }
    public Guid PropertyId { get; set; }

    public string Platform { get; set; } = null!;
    public string Link { get; set; } = null!;
    public string? Caption { get; set; }
    public DateTime? PostDate { get; set; }
}
