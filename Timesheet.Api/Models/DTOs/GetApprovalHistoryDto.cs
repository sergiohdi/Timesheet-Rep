using System;

namespace Timesheet.Api.Models.DTOs
{
    public class GetApprovalHistoryDto
    {
        public DateTime ActionDate { get; set; }
        public string ActionType { get; set; }
        public string UserName { get; set; }
        public string Comments { get; set; }
        public int TimesheetType { get; set; }
        public int? ApprovalId { get; set; }
    }
}
