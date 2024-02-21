namespace Timesheet.Client.Models
{
    public class CreateApprovalHistory
    {
        public int ActionType { get; set; }
        public int IdTimesheetControl { get; set; }
        public int TimesheetType { get; set; }
        public string Comments { get; set; }
    }
}
