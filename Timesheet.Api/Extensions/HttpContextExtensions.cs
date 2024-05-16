using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Timesheet.Api.Extensions;

public static class HttpContextExtensions
{
    public static string GetUserId(this HttpContext context) =>
        context.User.Claims.FirstOrDefault(c => c.Type.Equals("UserId")).Value.ToString();

    public static int GetUserIdInt(this HttpContext context) =>
        Convert.ToInt16(context.User.Claims.FirstOrDefault(c => c.Type.Equals("UserId")).Value.ToString());

    public static string GetUserName(this HttpContext context) =>
        context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value.ToString();

    public static string GetUserNameLog(this HttpContext context) =>
        context.User.Claims.FirstOrDefault(c => c.Type.Equals("NameLog")).Value.ToString();

    public static string GetUserEmail(this HttpContext context) =>
        context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();

    public static string GetUserRole(this HttpContext context) =>
        context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString();

    public static int GetUserRoleInt(this HttpContext context) =>
        Convert.ToInt16(context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString());
}
