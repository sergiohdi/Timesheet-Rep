using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations;

public class ApprovalStatusBusiness : IApprovalStatusBusiness
{
    private readonly IApprovalStatusRepository _approvalStatusRepository;

    public ApprovalStatusBusiness(IApprovalStatusRepository approvalStatusRepository) => _approvalStatusRepository = approvalStatusRepository;

    public IEnumerable<ApprovalStatusDto> GetApprovalStatuses() => _approvalStatusRepository.GetApprovalStatuses();
}
