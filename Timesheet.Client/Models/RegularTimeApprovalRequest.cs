using System;

namespace Timesheet.Client.Models;

public class RegularTimeApprovalRequest : Approval
{
    public DateTime Period { get; set; }
}
