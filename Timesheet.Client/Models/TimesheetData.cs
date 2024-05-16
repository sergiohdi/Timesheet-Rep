using System;

namespace Timesheet.Client.Models;

public class TimesheetData
{
    public int? ClientId { get; set; }
    public int? ProjectId { get; set; }
    public int? ActivityId { get; set; }
    public int? TimeOffId { get; set; }
    public DateTime EntryDate { get; set; }
    public decimal TotalHours { get; set; }
    public string Comments { get; set; }
    public int? ApprovalStatus { get; set; }
    public int? Billable { get; set; }
    public string Location { get; set; }
    public bool IsTimeOff { get; set; }
}
