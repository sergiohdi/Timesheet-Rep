namespace Timesheet.Api.Models.DTOs;

public class TimesheetDataFutureDto
{
    public long TimesheetId  { get; set; }
    public int TimeoffId { get; set; }
    public int Billable { get; set; }
    public decimal NonBillableHours { get; set; }
    public decimal BillableHours { get; set; }
    public decimal TimeOffHours { get; set; }
    public decimal ProjectHours { get; set; }
    public decimal TotalHours { get; set; }
}
