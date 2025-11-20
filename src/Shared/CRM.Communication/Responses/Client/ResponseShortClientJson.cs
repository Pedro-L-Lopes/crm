using CRM.Communication.Responses.Address;

namespace CRM.Communication.Responses.Client;

public class ResponseShortClientJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Document { get; set; } = null!;
    public string? Type { get; set; }
    public string? Gender { get; set; }
}
