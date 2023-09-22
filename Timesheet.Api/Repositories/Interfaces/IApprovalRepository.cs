using System.Collections.Generic;
using System;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces
{
    public interface IApprovalRepository
    {
        bool SaveApprovalRequest(ApprovalDto approvalRequest);
        IEnumerable<ApprovalDto> GetTimeOffRecords(DateTime period);
        ApprovalDto GetTimeoffById(int id);
        bool DeleteTimeoffRecord(ApprovalDto timeoff);
        bool UpdateTimeoffRecord(ApprovalDto approval);
        bool UpdateApprovalStatus(List<DateTime> periods, List<int> userIds);
        IEnumerable<ApprovalDto> GetTimeoffRequests(DateTime startDate, DateTime endDate, int supervisorId = 0);
    }
}