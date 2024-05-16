namespace Timesheet.Client.Models;

public class TimeOff
{
    public int TimeOffId { get; set; }
    public string TimeOffName { get; set; }
    public string TimeOffCode { get; set; }
    public bool? Disabled { get; set; }
}
