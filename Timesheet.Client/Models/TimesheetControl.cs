using System;

namespace Timesheet.Client.Models;

public class TimesheetControl
{
    public int TimesheetPeriodId { get; set; }
    public DateTime TimesheetPeriod { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ApprovalStatusId { get; set; }
    public bool IsSelected { get; set; }
    public string StartDateString { get; set; }
    public string EndDateString { get; set; }
    public int UserTemplateId { get; set; }
}
