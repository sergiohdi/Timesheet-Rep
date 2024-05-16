namespace Timesheet.Client.Models;

public class RegularTimeApproval : Approval
{
    public string UserName { get; set; }
    public string StatusName { get; set; }
    public int SupervisorId { get; set; }
    public bool IsSelected { get; set; }
    public string TimesheetPeriod { get; set; }
}
