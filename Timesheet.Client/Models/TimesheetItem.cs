using System.Collections.Generic;

namespace Timesheet.Client.Models
{
    public class TimesheetItem
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int? ActivityId { get; set; }  
        public string ActivityName { get; set; }
        public int? TimeOffId { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? Billable { get; set; }
        public string Location { get; set; }
        public bool IsTimeOff { get; set; }
        public string PONumber { get; set; }
        public string TotalHoursPerRow { get; set; }
        public List<TimesheetEntry> Entries { get; set; }
        public bool IsNewRecord { get; set; } = false;
        public bool EnableBillableDrop { get; set; } = true;
    }
}
