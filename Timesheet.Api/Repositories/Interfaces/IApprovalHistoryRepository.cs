using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces
{
    public interface IApprovalHistoryRepository
    {
        bool CreateApprovalHistory(CreateApprovalRequestDto request);
        IEnumerable<GetApprovalHistoryDto> GetApprovalsHistory(int timesheetId);
    }
}