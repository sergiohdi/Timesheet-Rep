namespace Timesheet.Api.Models.DTOs
{
    public class ApprovalRegularTimeRequestsDto : ApprovalDto
    {
        public string UserName { get; set; }
        public string StatusName { get; set; }
        public int SupervisorId { get; set; }
        public string TimesheetPeriod { get; set; }
    }
}
