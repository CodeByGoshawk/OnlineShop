﻿using Microsoft.AspNetCore.Authorization;

namespace OnlineShop.Backoffice.WebApiEndPoint.Authorizations.Requirements;

public class AdminsOrOwnerOnlyRequirement : IAuthorizationRequirement
{
}
