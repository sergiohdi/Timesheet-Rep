using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces
{
    public interface IApprovalDataService
    {
        Task<ApiResponse<bool>> CreateTimeoffRequest(TimeoffApproval request);
        Task<ApiResponse<bool>> CreateRegularTimeRequest(RegularTimeApprovalRequest request);
        Task<ApiResponse<IEnumerable<TimeoffApproval>>> GetTimeOffRecords(DateTime period);
        Task<ApiResponse<bool>> UpdateTimeoffRecord(int timeoffId, TimeoffApproval record);
        Task<ApiResponse<bool>> DeleteTimeoffRecord(int requestId);
        Task<ApiResponse<IEnumerable<TimeoffRequest>>> GetTimeOffRequests(DateTime startDate, DateTime endDate, bool isSupervisor = false);
        Task<ApiResponse<IEnumerable<RegularTimeApproval>>> GetRegularTimeRequests(DateTime startDate, DateTime endDate);
        Task<ApiResponse<bool>> ApproveTimeoffRequests(List<int> ids);
        Task<ApiResponse<bool>> ReopenTimeoffRequests(List<int> ids);
        Task<ApiResponse<bool>> RejectTimeoffSelected(List<int> ids);
        Task<ApiResponse<bool>> RejectTimesheetsRequests(List<int> ids);
        Task<ApiResponse<bool>> DeleteTimeoffRequests(List<int> ids);
    }
}