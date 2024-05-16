using System;

namespace Timesheet.Client.Models;

public class TimeEntryDetails
{
    public DateTime Date { get; set; }
    public string DateEntry { get; set; }
    public string ClientProject { get; set; }
    public string Task { get; set; }
    public string Activity { get; set; }
    public string Time { get; set; }
    public string Comments { get; set; }
    public string Location { get; set; }
}
