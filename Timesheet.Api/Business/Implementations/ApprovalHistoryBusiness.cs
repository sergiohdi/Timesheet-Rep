using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations
{
    public class ApprovalHistoryBusiness : IApprovalHistoryBusiness
    {
        private readonly IApprovalHistoryRepository _approvalHistoryRepository;

        public ApprovalHistoryBusiness(IApprovalHistoryRepository approvalHistoryRepository)
        {
            _approvalHistoryRepository = approvalHistoryRepository;
        }

        public IEnumerable<GetApprovalHistoryDto> GetApprovalsHistory(int timesheetId)
        {
            return _approvalHistoryRepository.GetApprovalsHistory(timesheetId);
        }

        public bool CreateApprovalHistory(CreateApprovalRequestDto request)
        {
            return _approvalHistoryRepository.CreateApprovalHistory(request);
        }
    }
}
