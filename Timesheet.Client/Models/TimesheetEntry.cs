using System;

namespace Timesheet.Client.Models;

public class TimesheetEntry
{
    public long Id { get; set; }
    public DateTime EntryDate { get; set; }
    public int Day { get; set; }
    public decimal TotalHours { get; set; }
    public string Comments { get; set; }
}
