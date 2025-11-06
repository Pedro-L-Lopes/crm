namespace CRM.Communication.Requests;
public class RequestRegisterTenantJson
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public Guid PlanId { get; set; }
    public DateTime PlanExpiration { get; set; }

}
