using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces;

public interface ITimesheetControlDataService
{
    Task<ApiResponse<IEnumerable<TimesheetControl>>> GetTimesheetControl();
    Task<ApiResponse<bool>> UpdateApprovalStatus(List<int> ids);
    Task<ApiResponse<TimesheetControl>> CreateTimesheetControl(DateTime period);
    Task<ApiResponse<IEnumerable<TimesheetControlApproval>>> GetTimesheetControlRequests(DateTime startDate, DateTime endDate, bool isSupervisor = false);
    Task<ApiResponse<bool>> ApproveTimesheetsRequests(List<int> ids);
    Task<ApiResponse<bool>> ReopenTimesheetsRequests(List<int> ids);
    Task<ApiResponse<bool>> DeleteTimesheetsRequests(List<int> ids);
    Task<ApiResponse<TimesheetControl>> GetTimesheetControlRecord(DateTime period, int userId = 0);
}
