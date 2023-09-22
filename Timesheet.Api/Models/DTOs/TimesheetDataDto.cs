using System;

namespace Timesheet.Api.Models.DTOs
{
    public class TimesheetDataDto
    {
        public long TimesheetId { get; set; }
        public int? ClientId { get; set; }
        public int? ProjectId { get; set; }
        public int? ActivityId { get; set; }
        public int? TimeOffId { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal TotalHours { get; set; }
        public string Comments { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? Billable { get; set; }
        public string Location { get; set; }
        public string PONumber { get; set; }
        public bool IsTimeOff 
        { 
            get 
            { 
                return this.ClientId is null && this.ProjectId is null && this.ActivityId is null; 
            } 
        }  
    }
}
