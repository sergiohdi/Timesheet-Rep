using Timesheet.Client.Utils;

namespace Timesheet.Client.Models
{
    public class UpdateTimesheetBasePropertiesRequest
    {
        public int UserId { get; set; }
        public TimesheetItemAction Action { get; set; }
        public Property Property { get; set; }
        public TimesheetItem TimesheetItem { get; set; }
        public bool IsBaseProperty { get; set; }
    }
}
