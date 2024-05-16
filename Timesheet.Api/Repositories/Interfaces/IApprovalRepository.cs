using System.Collections.Generic;
using System;
using Timesheet.Api.Models.DTOs;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Repositories.Interfaces;

public interface IApprovalRepository
{
    int SaveApprovalRequest(ApprovalDto approvalRequest);
    IEnumerable<ApprovalDto> GetTimeOffRecords(DateTime period, int userId);
    ApprovalDto GetTimeoffById(int timeoffApprovalId, int userId);
    bool DeleteTimeoffRecord(ApprovalDto timeoff);
    bool UpdateTimeoffRecord(ApprovalDto approval);
    bool UpdateApprovalStatus(List<DateTime> periods, List<int> userIds);
    IEnumerable<ApprovalTimeoffRequestDto> GetTimeoffRequests(DateTime startDate, DateTime endDate, int supervisorId = 0);
    IEnumerable<ApprovalRegularTimeRequestsDto> GetRegularTimeRequests(DateTime startDate, DateTime endDate, int supervisorId);
    bool ProcessRequests(int[] ids, int status);
    IEnumerable<ApprovalDto> GetApprovalRecords(int[] ids);
    bool DeleteApprovalRequests(int[] ids);
    bool DeleteTimesheetRecordApproval(DateTime period, int[] userIds);
    IEnumerable<ApprovalDto> GetRegularTimeApprovals(int[] userIds, int[] timesheetControlIds);
    List<ApprovalDto> GetFutureApprovals(DateTime currentPeriod, int userId);
    bool UpdateApprovals(IEnumerable<ApprovalDto> approvals);
    bool ApprovalActionRegularTimeRecords(int[] ids, ApprovalStatusOption approvalStatus, int userId, string userName);
}