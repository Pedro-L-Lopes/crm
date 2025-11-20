using CRM.Domain.Enums;
using CRM.Domain.Services.LoggedUser;
using CRM.Exceptions;
using CRM.Exceptions.ExceptionsBase;

namespace CRM.Application.Services.Validator;

public class RoleValidator : IRoleValidator
{
    private readonly ILoggedUser _loggedUser;

    // Mapeamento de permissões por campo
    private readonly Dictionary<(Role Role, string Entity, string Field), bool> _fieldPermissions = new()
    {
        // ==================== USER ====================
        [(Role.superAdmin, "User", "TenantId")] = true,
        [(Role.owner, "User", "TenantId")] = false,
        [(Role.admin, "User", "TenantId")] = false,
        [(Role.agent, "User", "TenantId")] = false,

        [(Role.superAdmin, "User", "Role")] = true,
        [(Role.owner, "User", "Role")] = true,
        [(Role.admin, "User", "Role")] = true,
        [(Role.agent, "User", "Role")] = false,

        // ==================== CLIENT ====================
        [(Role.superAdmin, "Client", "TenantId")] = true,
        [(Role.owner, "Client", "TenantId")] = false,
        [(Role.admin, "Client", "TenantId")] = false,
        [(Role.agent, "Client", "TenantId")] = false,

        [(Role.superAdmin, "Client", "AgentId")] = true,
        [(Role.owner, "Client", "AgentId")] = true,
        [(Role.admin, "Client", "AgentId")] = true,
        [(Role.agent, "Client", "AgentId")] = false,

        // ==================== PROPERTY ====================
        [(Role.superAdmin, "Property", "TenantId")] = true,
        [(Role.owner, "Property", "TenantId")] = false,
        [(Role.admin, "Property", "TenantId")] = false,
        [(Role.agent, "Property", "TenantId")] = false,

        [(Role.superAdmin, "Property", "Status")] = true,
        [(Role.owner, "Property", "Status")] = true,
        [(Role.admin, "Property", "Status")] = true,
        [(Role.agent, "Property", "Status")] = false,

        // ==================== TENANT ====================
        [(Role.superAdmin, "Tenant", "PlanId")] = true,
        [(Role.owner, "Tenant", "PlanId")] = false,
        [(Role.admin, "Tenant", "PlanId")] = false,
        [(Role.agent, "Tenant", "PlanId")] = false,
    };

    public RoleValidator(ILoggedUser loggedUser)
    {
        _loggedUser = loggedUser;
    }

    public async Task<Role> GetCurrentRoleAsync()
    {
        var user = await _loggedUser.User();

        if (Enum.TryParse<Role>(user.Role, ignoreCase: true, out var role))
            return role;

        throw new CRMException("Role do usuário é inválida");
    }

    public async Task<(Guid UserId, Guid TenantId, Role Role)> GetUserContextAsync()
    {
        var user = await _loggedUser.User();
        var role = await GetCurrentRoleAsync();

        return (user.Id, user.TenantId, role);
    }

    public async Task EnsureHasRoleAsync(params Role[] allowedRoles)
    {
        if (allowedRoles is null || allowedRoles.Length == 0)
            return; // Sem restrição

        var currentRole = await GetCurrentRoleAsync();

        if (!allowedRoles.Contains(currentRole))
        {
            throw new UnauthorizedException(
                ResourceMessageException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE
            );
        }
    }

    public bool CanModifyField(Role role, string entity, string fieldName)
    {
        var key = (role, entity, fieldName);

        if (_fieldPermissions.TryGetValue(key, out var canModify))
            return canModify;

        // Se não estiver mapeado, permite por padrão
        return true;
    }
}
