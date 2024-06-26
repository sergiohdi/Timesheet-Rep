﻿using System;
using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces;

public interface ITimesheetControlRepository
{
    IEnumerable<TimesheetControlDto> GetTimesheetControl();
    bool UpdateApprovalStatus(int[] ids);
    IEnumerable<TimesheetControlDto> GetTimesheetControlById(int[] ids);
    TimesheetControlDto GetTimesheetControlByPeriodAndUserId(DateTime period, int userId);
    TimesheetControlDto CreateTimesheetControlRecord(TimesheetControlDto record);
    IEnumerable<TimesheetControlApprovalDto> GetTimesheetControlRequests(DateTime startDate, DateTime endDate, int supervisorId = 0);
    bool ProcessTimesheetsRequests(int[] ids, int status);
    IEnumerable<TimesheetControlApprovalDto> GetTimesheetsApprovalRecords(int[] ids);
    bool UpdateTimesheetContolRecords(int timesheetControlId, int status);
    TimesheetControlDto GetTimesheetControlRecord(DateTime period, int userId);
    List<TimesheetControlDto> GetFutureTimesheetControlRecords(DateTime currentPeriod, int userId);
    bool UpdateUserTemplate(int[] ids, int newUserTemplate);
}
