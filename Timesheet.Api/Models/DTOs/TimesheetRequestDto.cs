using Microsoft.VisualBasic;

namespace Timesheet.Api.Models.DTOs
{
    public class TimesheetRequestDto
    {
        public int UserId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Period { get; set; }
    }
}
