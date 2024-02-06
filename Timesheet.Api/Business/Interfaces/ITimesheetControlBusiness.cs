using System;
using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces
{
    public interface ITimesheetControlBusiness
    {
        IEnumerable<TimesheetControlDto> GetTimesheetControl();
        bool UpdateApprovalStatus(int[] ids);
        TimesheetControlDto CreateTimesheetControlRecord(DateTime period, int userId);
        IEnumerable<TimesheetControlApprovalDto> GetTimesheetControlRequests(DateTime startDate, DateTime endDate, int userId = 0);
        bool ApproveTimesheetsRequests(int[] ids, bool isWTF = true);
        bool ReopenTimesheetsRequests(int[] ids, bool isWTF = true);
        bool DeleteTimesheetsRequests(int[] ids);
        TimesheetControlDto GetTimesheetControlRecord(DateTime period, int userId);

    }
}
