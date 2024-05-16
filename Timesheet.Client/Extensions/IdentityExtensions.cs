using System;
using System.Linq;
using System.Security.Claims;

namespace Timesheet.Client.Extensions;

public static class IdentityExtensions
{
    public static bool GetForceChangePassword(this ClaimsPrincipal user) =>
        Convert.ToBoolean(user.Claims.FirstOrDefault(x => x.Type.Equals("EnforceChangePassword"))?.Value);

    public static string GetUserEmailId(this ClaimsPrincipal user) =>
        user.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;

    public static string GetUserRole(this ClaimsPrincipal user) =>
        user.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role))?.Value;

}
