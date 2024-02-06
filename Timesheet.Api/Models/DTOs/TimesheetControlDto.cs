using System;

namespace Timesheet.Api.Models.DTOs
{
    public class TimesheetControlDto
    {
        public int TimesheetPeriodId { get; set; }
        public DateTime TimesheetPeriod { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ApprovalStatusId { get; set; }
        public int UserTemplateId { get; set; }
    }
}
