namespace Timesheet.Api.Models.DTOs;

public class TimesheetControlApprovalDto : TimesheetControlDto
{
    public string UserName { get; set; }
    public string StatusName { get; set; }
    public bool IsApproved { get; set; }
}
