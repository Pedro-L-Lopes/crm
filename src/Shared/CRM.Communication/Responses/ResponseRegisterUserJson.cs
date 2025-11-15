using CRM.Communication.Responses;
using System.Text.Json.Serialization;

namespace CRM.Communication.Requests;
public class ResponseRegisterUserJson
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string TenantName { get; set; } = string.Empty;
    public string TenantType { get; set; } = string.Empty;
    public DateTime? PlanExpiration { get; set; }
    [JsonIgnore]
    public DateTime LastLogin { get; set; }
    public ReponseTokenJson Tokens { get; set; } = default!; 
}
