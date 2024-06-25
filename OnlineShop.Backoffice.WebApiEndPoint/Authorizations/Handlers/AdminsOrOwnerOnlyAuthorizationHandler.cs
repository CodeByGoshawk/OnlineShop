using Microsoft.AspNetCore.Authorization;
using OnlineShop.Backoffice.WebApiEndPoint.Authorizations.Requirements;
using PublicTools.Attributes;
using PublicTools.Constants;
using System.Security.Claims;

namespace OnlineShop.Backoffice.WebApiEndPoint.Authorizations.Handlers;

public class AdminsOrOwnerOnlyAuthorizationHandler : AuthorizationHandler<AdminsOrOwnerOnlyRequirement, object>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminsOrOwnerOnlyRequirement requirement, object resource)
    {
        var requesterUserRoles = context.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
        if (requesterUserRoles.Contains(DatabaseConstants.DefaultRoles.GodAdminName) ||
           requesterUserRoles.Contains(DatabaseConstants.DefaultRoles.AdminName))
        {
            context.Succeed(requirement);
            return;
        }

        var requesterUserId = context.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid)!.Value;
        var resourceOwnerIdProperty = resource.GetType().GetProperties().SingleOrDefault(p => p.IsDefined(typeof(RequesterIdAttribute), false));

        if (resourceOwnerIdProperty is not null && requesterUserId == resourceOwnerIdProperty!.GetValue(resource)!.ToString()) context.Succeed(requirement);
        else
        {
            var method = resource.GetType().GetMethod("GetOwnerId");
            var result = await (Task<List<string>>)method!.Invoke(resource, null)!;

            if (result is not null && result.Contains(requesterUserId)) context.Succeed(requirement);
        }

        return;
    }
}
