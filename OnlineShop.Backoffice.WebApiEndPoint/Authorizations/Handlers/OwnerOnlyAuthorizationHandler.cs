using Microsoft.AspNetCore.Authorization;
using OnlineShop.Backoffice.WebApiEndPoint.Authorizations.Requirements;
using PublicTools.Attributes;
using System.Security.Claims;

namespace OnlineShop.Backoffice.WebApiEndPoint.Authorizations.Handlers;

public class OwnerOnlyAuthorizationHandler : AuthorizationHandler<OwnerOnlyRequirement, object>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerOnlyRequirement requirement, object resource)
    {
        var requesterUserId = context.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid)!.Value;
        var resourceOwnerIdProperty = resource.GetType().GetProperties().SingleOrDefault(p => p.IsDefined(typeof(OwnerIdAttribute), false));

        if (resourceOwnerIdProperty is not null && requesterUserId == resourceOwnerIdProperty!.GetValue(resource)!.ToString()) context.Succeed(requirement);
        else
        {
            var method = resource.GetType().GetMethod("GetOwnerId");
            var result = await (Task<List<string>>)method!.Invoke(resource, null)!;

            if (result is null || result.Contains(requesterUserId)) context.Succeed(requirement);
        }

        return;
    }
}
