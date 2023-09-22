using System;
using System.Collections.Generic;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Utils;

namespace Timesheet.Api.Repositories.Interfaces
{
    public interface ITimesheetDataRepository
    {
        IEnumerable<TimesheetDataDto> GetTimesheetData(TimesheetRequestDto request);
        TimesheetDataDto GetTimesheetRecord(long timesheetId);
        void UpdateTimesheetBaseInformation(TimesheetItemDto item, Property property, object propertyData, int oldBillableInfo);
        void DeleteTimesheetRecords(long[] ids);
        long SetTimesheetRecord(Dictionary<string, object> data);
        bool ValidateTimesheetByProjectId(int projectId);
        bool ValidateTimesheetByActivityId(int activityId);
        bool ValidateTimesheetByUserId(int userId);
        void DeleteTimesheetRecord(long id);
        bool SaveTimeOffRecords(IEnumerable<RpTimeSheetData> timeOffRecords);
        void DeleteTimesheetOffRecords(int id, DateTime startDate, DateTime endDate);
        bool UpdateApprovedRecords(List<DateTime> periods, List<int> userIds);
    }
}