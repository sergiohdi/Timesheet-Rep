using System;

namespace Timesheet.Api.Models.DTOs;

public class CreateApprovalRequestDto
{
    public DateTime ActionDate { get; set; }
    public int ActionType { get; set; }
    public int IdUser { get; set; }
    public string UserName { get; set; }
    public int? IdTimesheetControl { get; set; }
    public int? ApprovalId { get; set; }
    public int TimesheetType { get; set; }
    public string Comments { get; set; }
}
