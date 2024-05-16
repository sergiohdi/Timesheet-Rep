using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Models.Configuration;

namespace Timesheet.Api.Utils;

public static class JWT
{
    public static string GenerateJwtToken(UserDto user, JwtSettings settings)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("UserId", user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim("NameLog", $"{user.LastName}, {user.FirstName}"),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.RoleId.ToString()),
            new Claim("IssuedOn", DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
            new Claim("EnforceChangePassword", user.ForcePasswordChange.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: settings.Issuer,
            audience: settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes( Convert.ToDouble(settings.ExpirationInMinutes)),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
