using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Extensions;

namespace Timesheet.Api.CustomFilters;

[AttributeUsage(AttributeTargets.All)]
public class AuthorizeRolesAttribute : Attribute, IAuthorizationFilter
{
    private readonly List<int> roles;

    public AuthorizeRolesAttribute(params int[] roles) => this.roles = roles.ToList();

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        _ = int.TryParse(context.HttpContext.GetUserRole(), out int roleId);
        if (roleId == 0)
        {
            context.Result = new ForbidResult();
            return;
        }

        if (!roles.Contains(roleId))
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}

