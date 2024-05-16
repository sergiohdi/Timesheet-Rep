using System;
using System.Collections.Generic;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Utils;

namespace Timesheet.Api.Repositories.Interfaces;

public interface ITimesheetDataRepository
{
    IEnumerable<TimesheetDataDto> GetTimesheetData(TimesheetRequestDto request);
    TimesheetDataDto GetTimesheetRecord(long timesheetId);
    void UpdateTimesheetBaseInformation(TimesheetItemDto item, Dictionary<Property, object> propertyData, int oldBillableInfo);
    void DeleteTimesheetRecords(long[] ids);
    long SetTimesheetRecord(Dictionary<string, object> data);
    bool ValidateTimesheetByProjectId(int projectId);
    bool ValidateTimesheetByActivityId(int activityId);
    bool ValidateTimesheetByUserId(int userId);
    void DeleteTimesheetRecord(long id);
    bool SaveTimeOffRecords(IEnumerable<TimesheetData> timeOffRecords);
    void DeleteTimesheetOffRecords(int id, int userId, DateTime startDate, DateTime endDate);
    bool UpdateTimesheetRecords(int id, int userId, DateTime startDate, DateTime endDate, int status);
    bool UpdateRegularRecords(int id, DateTime startDate, DateTime endDate, int status);
    bool UpdateRegularRecords(int userId, DateTime period, int status);
    bool DeleteTimesheetRecord(DateTime period, int userId);
    bool DeleteTimesheetOnlyRegularTime(DateTime period, int[] userIds);
    IEnumerable<TimesheetDataDto> GetRegularTimeData(DateTime period, int userId);
    List<TimesheetDataFutureDto> GetFutureTimesheetDataRecords(DateTime currentPeriod, int userId);
    bool UpdateFutureTimesheetRecords(IEnumerable<TimesheetDataFutureDto> records);
}