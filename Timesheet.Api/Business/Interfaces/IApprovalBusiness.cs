using System.Collections.Generic;
using System;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces
{
    public interface IApprovalBusiness
    {
        bool CreateTimeoffRequest(ApprovalDto approvalRequest);
        bool CreateRegularTimeRequest(ApprovalDto approvalRequest, int userId);
        IEnumerable<ApprovalDto> GetTimeOffRecords(DateTime period);
        bool UpdateTimeoffRecord(ApprovalDto approval);
        bool DeleteTimeoffRecords(int requestId);
        IEnumerable<ApprovalTimeoffRequestDto> GetTimeOffRequests(DateTime startDate, DateTime endDate, int userId = 0);
        IEnumerable<ApprovalDto> GetRegularTimeRequests(DateTime startDate, DateTime endDate, int userId = 0);
        bool ApproveTimeoffRequests(int[] ids, bool isWTF = true);
        bool ReopenTimeoffRequests(int[] ids);
        bool RejectTimeoffSelected(int[] ids);
        bool RejectTimesheetsRequests(int[] ids);
        bool DeleteTimeoffRequests(int[] ids);
    }
}