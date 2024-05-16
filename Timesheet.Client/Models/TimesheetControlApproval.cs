namespace Timesheet.Client.Models;

public class TimesheetControlApproval : TimesheetControl
{
    public string UserName { get; set; }
    public string StatusName { get; set; }
    public int SupervisorId { get; set; }
}
