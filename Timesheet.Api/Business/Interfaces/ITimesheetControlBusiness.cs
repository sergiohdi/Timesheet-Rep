using System;
using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces;

public interface ITimesheetControlBusiness
{
    IEnumerable<TimesheetControlDto> GetTimesheetControl();
    IEnumerable<TimesheetControlApprovalDto> GetTimesheetControlRequests(DateTime startDate, DateTime endDate);
    TimesheetControlDto GetTimesheetControlRecord(DateTime period, int userId);
    TimesheetControlDto CreateTimesheetControlRecord(DateTime period, int userId);
    bool UpdateApprovalStatus(int[] ids);
    bool ApproveTimesheetsRequests(int[] ids, int loggedUserId, string loggedUserName);
    bool ReopenTimesheetsRequests(int[] ids, int loggedUserId, string loggedUserName);
    bool DeleteTimesheetsRequests(int[] ids);
}
