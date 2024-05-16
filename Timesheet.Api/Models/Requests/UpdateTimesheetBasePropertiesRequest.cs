using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Utils;

namespace Timesheet.Api.Models.Requests;

public class UpdateTimesheetBasePropertiesRequest
{
    public TimesheetItemAction Action { get; set; }
    public Property Property { get; set; }
    public TimesheetItemDto TimesheetItem { get; set; }
    public bool IsBaseProperty { get; set; }
}
