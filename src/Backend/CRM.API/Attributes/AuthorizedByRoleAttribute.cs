using CRM.API.Filters;
using CRM.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CRM.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizedByRoleAttribute : TypeFilterAttribute
{
    public AuthorizedByRoleAttribute(params Role[] acceptedRoles) : base(typeof(AuthorizedByRoleFilter))
    {
        Arguments = new object[] { acceptedRoles };
    }
}