using System;

namespace Timesheet.Client.Models;

public class GetApprovalsHistory
{
    public DateTime ActionDate { get; set; }
    public string ActionType { get; set; }
    public string UserName { get; set; }
    public string Comments { get; set; }
    public int TimesheetType { get; set; }
    public int? ApprovalId { get; set; }
}
