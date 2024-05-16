using System.ComponentModel.DataAnnotations;

namespace Timesheet.Api.Models.DTOs;

public class ChangePasswordDto
{
    [Required]
    public string EmailId { get; set; }
    [Required]
    public string CurrentPassword { get; set; }
    [Required]
    public string NewPassword { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
}
