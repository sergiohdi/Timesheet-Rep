using System;

namespace Timesheet.Client.Models
{
    public abstract class Approval
    {
        public int ApprovalId { get; set; }
        public int UserId { get; set; }
        public int ApprovalStatusId { get; set; }
        public int ApprovalType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }        
    }
}
