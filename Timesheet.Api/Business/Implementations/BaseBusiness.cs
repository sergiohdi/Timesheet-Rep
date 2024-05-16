using System;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Business.Implementations;

public abstract class BaseBusiness
{
    private readonly IApprovalHistoryRepository _approvalHistoryRepository;

    protected BaseBusiness(IApprovalHistoryRepository approvalHistoryRepository) =>
        _approvalHistoryRepository = approvalHistoryRepository;

    public bool CreateApprovalHistoryRecord(
       int status,
       int userId,
       string userNameLog,
       int timesheetControlId,
       int approvalId,
       ApprovalType approvalType
   )
    {
        return _approvalHistoryRepository.CreateApprovalHistory(new CreateApprovalRequestDto
        {
            ActionDate = DateTime.Now,
            ActionType = status,
            IdUser = userId,
            UserName = userNameLog,
            IdTimesheetControl = timesheetControlId,
            ApprovalId = approvalId,
            TimesheetType = (int)approvalType
        });
    }
}
