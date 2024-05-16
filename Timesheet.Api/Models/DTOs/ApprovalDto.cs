using System;

namespace Timesheet.Api.Models.DTOs;

public class ApprovalDto
{
    public int ApprovalId { get; set; }
    public int UserId { get; set; }
    public int ApprovalStatusId { get; set; }
    public int? TimeOffId { get; set; }
    public int ApprovalType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? Duration { get; set; }
    public string Comments { get; set; }
    public DateTime? Period { get; set; }
    public int? TimesheetControlId { get; set; }
}
