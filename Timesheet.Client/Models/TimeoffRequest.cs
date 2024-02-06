namespace Timesheet.Client.Models
{
    public class TimeoffRequest : TimeoffApproval
    {
        public string UserName { get; set; }
        public string StatusName { get; set; }
        public string TimeoffName { get; set; }
        public int SupervisorId { get; set; }
        public bool IsSelected { get; set; }
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
    }
}
