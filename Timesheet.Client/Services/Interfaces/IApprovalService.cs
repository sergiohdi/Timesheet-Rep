using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces
{
    public interface IApprovalService
    {
        Task<ApiResponse<bool>> CreateTimeoffRequest(TimeoffApproval request);
        Task<ApiResponse<IEnumerable<TimeoffApproval>>> GetTimeOffRecords(DateTime period);
        Task<ApiResponse<bool>> UpdateTimeoffRecord(int timeoffId, TimeoffApproval record);
        Task<ApiResponse<bool>> DeleteTimeoffRecord(int requestId);
        Task<ApiResponse<IEnumerable<TimeoffApproval>>> GetTimeOffRequests(DateTime startDate, DateTime endDate, int userId = 0);
    }
}