using Microsoft.AspNetCore.Authorization;
using OnlineShop.Office.Application.Framework.Abstracts;
using OnlineShop.Office.WebApiEndPoint.Authorizations.Requirements;
using System.Security.Claims;

namespace OnlineShop.Office.WebApiEndPoint.Authorizations.Handlers;

public class SelfDataAuthorizationHandler : AuthorizationHandler<SelfDataRequirement, ISelfRequestDto>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SelfDataRequirement requirement, ISelfRequestDto resource)
    {
        var requesterUserId = context.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid)!.Value;

        if (requesterUserId == resource.Id) context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
