namespace CRM.Communication.Requests;
public class RequestRegisterUserJson
{
    public Guid TenantId { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime LastLogin {  get; set; }
    public Boolean IsActive { get; set; } = true;
}
