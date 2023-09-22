using System.Collections.Generic;

namespace Timesheet.Api.Models.DTOs
{
    public class TimesheetItemDto
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public int? ProjectId { get; set; }
        public int? ActivityId { get; set; }
        public int? TimeOffId { get; set; }
        public int? ApprovalStatus { get; set;}
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
        public List<TimesheetEntryDto> Entries { get; set; }
    }
}
