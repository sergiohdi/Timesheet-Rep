using System.Collections.Generic;
using System;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces;

public interface IApprovalBusiness
{
    bool CreateTimeoffRequest(ApprovalDto approvalRequest, int userId, string userName);
    bool CreateRegularTimeRequest(ApprovalDto approvalRequest, int userId, string userName);
    IEnumerable<ApprovalDto> GetTimeOffRecords(DateTime period, int userId);
    bool UpdateTimeoffRecord(ApprovalDto approval, int userId, string userName);
    bool DeleteTimeoffRecords(int requestId, int userId);
    IEnumerable<ApprovalTimeoffRequestDto> GetTimeOffRequests(DateTime startDate, DateTime endDate, int userId = 0);
    IEnumerable<ApprovalDto> GetRegularTimeRequests(DateTime startDate, DateTime endDate, int userId = 0);
    bool ApproveTimeoffRequests(int[] ids, int userId, string userName);
    bool ReopenTimeoffRequests(int[] ids, int userId, string userName);
    bool RejectTimeoffSelected(int[] ids, int userId, string userName);
    bool RejectTimesheetsRequests(int[] ids);
    bool DeleteTimeoffRequests(int[] ids);
    bool ApproveRegularTimeRequests(int[] ids, int userId, string userName);
    bool RejectRegularTimeRequests(int[] ids, int userId, string userName);
}