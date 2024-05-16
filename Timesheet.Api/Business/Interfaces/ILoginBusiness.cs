using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces;

public interface ILoginBusiness
{
    LoginResponseDto VerifyLogin(LoginDto login);
    LoginResponseDto VerifyChangePassword(ChangePasswordDto login);
    bool UpdatePassword(ChangePasswordDto entity);
}



