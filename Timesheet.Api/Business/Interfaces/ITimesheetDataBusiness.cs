using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Utils;

namespace Timesheet.Api.Business.Interfaces;

public interface ITimesheetDataBusiness
{
    IEnumerable<TimesheetItemDto> GetTimesheetData(TimesheetRequestDto request);
    void UpdateTimesheetBaseInformation(TimesheetItemDto record, Property property);
    void DeleteTimesheetRecords(TimesheetItemDto record);
    long UpdateTimesheetHours(int userId, TimesheetItemDto record);
}