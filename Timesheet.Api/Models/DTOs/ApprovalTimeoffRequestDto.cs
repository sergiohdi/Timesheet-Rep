namespace Timesheet.Api.Models.DTOs
{
    public class ApprovalTimeoffRequestDto : ApprovalDto
    {
        public string UserName { get; set; }
        public string StatusName { get; set; }
        public string TimeoffName { get; set; }
        public int SupervisorId { get; set; }
    }
}
