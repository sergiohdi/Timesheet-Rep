using System.Collections.Generic;
using System;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces
{
    public interface IApprovalBusiness
    {
        bool SaveTimeOffRequest(ApprovalDto approvalRequest);
        IEnumerable<ApprovalDto> GetTimeOffRecords(DateTime period);
        bool UpdateTimeoffRecord(ApprovalDto approval);
        bool DeleteTimeoffRecords(int requestId);
        IEnumerable<ApprovalDto> GetTimeOffRequests(DateTime startDate, DateTime endDate, int userId = 0);
    }
}