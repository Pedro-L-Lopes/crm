using CRM.Communication.Responses;
using CRM.Domain.Enums;
using CRM.Domain.Repositories.User;
using CRM.Domain.Security.Tokens;
using CRM.Exceptions;
using CRM.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Linq;

namespace CRM.API.Filters;

public class AuthorizedByRoleFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUserReadOnlyRepository _repository;
    private readonly Role[] _acceptedRoles;

    public AuthorizedByRoleFilter(
        IAccessTokenValidator accessTokenValidator,
        IUserReadOnlyRepository repository,
        Role[] acceptedRoles)
    {
        _accessTokenValidator = accessTokenValidator;
        _repository = repository;
        _acceptedRoles = acceptedRoles;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);
            var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

            var user = await _repository.GetUserByIdentifier(userIdentifier);

            if (user == null)
            {
                throw new UnauthorizedException(ResourceMessageException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
            }

            if (_acceptedRoles.Length > 0)
            {
                if (!Enum.TryParse<Role>(user.Role, ignoreCase: true, out var userRole) ||
                    !_acceptedRoles.Contains(userRole))
                {
                    throw new UnauthorizedException(ResourceMessageException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
                }
            }

            //if (_acceptedRoles.Length > 0 && Array.IndexOf(_acceptedRoles, user.Role) == -1)
            //{
            //    throw new UnauthorizedException(ResourceMessageException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
            //}
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("TokenIsExpired")
            {
                TokenIsExpired = true,
            });
        }
        catch (CRMException ex)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessageException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authentication))
        {
            throw new CRMException(ResourceMessageException.NO_TOKEN);
        }

        return authentication["Bearer ".Length..].Trim();
    }
}