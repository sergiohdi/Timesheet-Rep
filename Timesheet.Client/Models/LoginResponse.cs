namespace Timesheet.Client.Models;

public class LoginResponse
{
    public bool IsSuccess { get; set; }
    public string Token { get; set; }
    public bool ForcePasswordChange { get; set; }
}
