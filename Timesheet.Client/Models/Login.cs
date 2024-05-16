using System.ComponentModel.DataAnnotations;

namespace Timesheet.Client.Models;

public class Login
{
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
    public string Email { get; set; }


    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    //[MinLength(12, ErrorMessage = "Your password should be at least 12 characters.")]
    //[RegularExpression(
    //    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{12,}$",
    //    ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}
