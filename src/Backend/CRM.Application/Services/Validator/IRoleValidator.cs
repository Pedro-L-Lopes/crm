using CRM.Domain.Enums;

namespace CRM.Application.Services.Validator;

public interface IRoleValidator
{
    Task<Role> GetCurrentRoleAsync();
    Task<(Guid UserId, Guid TenantId, Role Role)> GetUserContextAsync();
    Task EnsureHasRoleAsync(params Role[] allowedRoles);
    bool CanModifyField(Role role, string entity, string fieldName);
}
