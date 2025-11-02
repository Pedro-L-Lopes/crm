namespace CRM.Communication.Requests;
public class RequestRegisterTenantJson
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public Guid PlanId { get; set; } = Guid.Empty;

}
