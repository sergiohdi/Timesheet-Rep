using Microsoft.Extensions.Options;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.Configuration;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Api.Utils;

namespace Timesheet.Api.Business.Implementations;

public class LoginBusiness : ILoginBusiness
{
    private readonly IUserRepository _userRepository;
    private readonly JwtSettings _jwtSettings;

    public LoginBusiness(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings)
    {
        _userRepository = userRepository;
        _jwtSettings = jwtSettings.Value;
    }

    public LoginResponseDto VerifyLogin(LoginDto login)
    {
        UserDto existingUser = _userRepository.GetUserByEmail(login.Email);
        if (existingUser == null)
        {
            return new LoginResponseDto(false, string.Empty, false);
        }

        // Todo: get back this commented code
        // Compare hashed password with existing user password
        if (!BCrypt.Net.BCrypt.Verify(login.Password, existingUser.Password))
        {
            return new LoginResponseDto(false, string.Empty, false);
        }

        // Generate a jwt - token
        string jwtToken = JWT.GenerateJwtToken(existingUser, _jwtSettings);

        return new LoginResponseDto(true, jwtToken, existingUser.ForcePasswordChange.Value);
    }

    public LoginResponseDto VerifyChangePassword(ChangePasswordDto login)
    {
        UserDto existingUser = _userRepository.GetUserByEmail(login.EmailId);
        if (existingUser == null)
        {
            return new LoginResponseDto(false, string.Empty, false);
        }

        // Compare hashed password with existing user password
        if (!BCrypt.Net.BCrypt.Verify(login.CurrentPassword, existingUser.Password))
        {
            return new LoginResponseDto(false, string.Empty, false);
        }

        return new LoginResponseDto(true, string.Empty, true);
    }

    public bool UpdatePassword(ChangePasswordDto entity)
    {
        var user = _userRepository.GetUserByEmail(entity.EmailId);
        if (user == null)
        {
            return false;
        }

        // Compare hashed password with existing user password
        if (!BCrypt.Net.BCrypt.Verify(entity.CurrentPassword, user.Password))
        {
            return false;
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(entity.NewPassword);
        user.Password = hashedPassword;
        user.ForcePasswordChange = false;

        bool successUpdate = _userRepository.UpdatePassword(user);

        return successUpdate;
    }
}
