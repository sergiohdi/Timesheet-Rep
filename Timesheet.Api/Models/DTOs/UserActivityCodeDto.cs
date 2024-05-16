namespace Timesheet.Api.Models.DTOs;

public class UserActivityCodeDto : UserActivityCodeLightDto
{
    public int UserActivityCodeId { get; set; }
    public bool? IsActivityEnabled { get; set; }
    public int? UserId { get; set; }
    public bool? IsSampleUser { get; set; }
    public bool? IsUserDisabled { get; set; }
    public string UserTitle { get; set; }
    public string PositionTitle { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DepartmentName { get; set; }
    public bool? IsPrimaryDepartment { get; set; }
    public string CostCenterGroup { get; set; }
    public bool? Disabled { get; set; }
}
