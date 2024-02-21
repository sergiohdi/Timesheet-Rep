using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces
{
    public interface IApprovalHistoryBusiness
    {
        bool CreateApprovalHistory(CreateApprovalRequestDto request);
        IEnumerable<GetApprovalHistoryDto> GetApprovalsHistory(int timesheetId);
    }
}