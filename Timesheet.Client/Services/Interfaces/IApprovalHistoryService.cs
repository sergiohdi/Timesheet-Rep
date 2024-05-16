using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces;

public interface IApprovalHistoryService
{
    // Todo: check if this code is still needed
    // Task<ApiResponse<bool>> CreateTimeoffRequest(CreateApprovalHistory request);
    Task<ApiResponse<List<GetApprovalsHistory>>> GetApprovalsHistory(int timesheetId);
}