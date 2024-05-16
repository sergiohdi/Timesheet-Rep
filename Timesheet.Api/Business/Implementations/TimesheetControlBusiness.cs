using System;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Business.Implementations;

public class TimesheetControlBusiness : BaseBusiness, ITimesheetControlBusiness
{
    private readonly ITimesheetControlRepository _timesheetControlRepository;
    private readonly IApprovalRepository _approvalRepository;
    private readonly ITimesheetDataRepository _timesheetDataRepository;
    private readonly IUserRepository _userRepository;
    private readonly IApprovalHistoryRepository _approvalHistoryRepository;

    public TimesheetControlBusiness(
        ITimesheetControlRepository timesheetControlRepository,
        IApprovalRepository approvalRepository,
        ITimesheetDataRepository timesheetDataRepository,
        IUserRepository userRepository,
        IApprovalHistoryRepository approvalHistoryRepository
    ) : base(approvalHistoryRepository)
    {
        _timesheetControlRepository = timesheetControlRepository;
        _approvalRepository = approvalRepository;
        _timesheetDataRepository = timesheetDataRepository;
        _userRepository = userRepository;
        _approvalHistoryRepository = approvalHistoryRepository;
    }

    public IEnumerable<TimesheetControlDto> GetTimesheetControl() => _timesheetControlRepository.GetTimesheetControl();

    public TimesheetControlDto GetTimesheetControlRecord(DateTime period, int userId) => _timesheetControlRepository.GetTimesheetControlRecord(period, userId);

    // Todo: check if we still need this method
    public bool UpdateApprovalStatus(int[] ids)
    {
        //IEnumerable<TimesheetControlDto> records = _timesheetControlRepository.GetTimesheetControlById(ids);

        //// Update timesheetcontrol table
        //_timesheetControlRepository.UpdateApprovalStatus(ids);

        //List<int> userIds = records.Select(x => x.UserId).Distinct().ToList();
        //List<DateTime> periods = records.Select(x => x.TimesheetPeriod).Distinct().ToList();

        //// Update approvals table
        //_approvalRepository.UpdateApprovalStatus(periods, userIds);

        //// Update timesheet table
        //_timesheetDataRepository.UpdateApprovedRecords(periods, userIds);

        return true;
    }

    public TimesheetControlDto CreateTimesheetControlRecord(DateTime period, int userId)
    {
        UserDto existingUser = _userRepository.GetUser(userId);
        if (existingUser == null)
        {
            return null;
        }

        TimesheetControlDto response = _timesheetControlRepository.GetTimesheetControlByPeriodAndUserId(period, userId);
        if (response != null)
        {
            return response;
        }

        return _timesheetControlRepository.CreateTimesheetControlRecord(new TimesheetControlDto
        {
            TimesheetPeriod = period,
            UserId = userId,
            StartDate = period,
            EndDate = DateFunctions.GetPeriodLastDate(period),
            ApprovalStatusId = (int)ApprovalStatusOption.NotSubmitted,
            UserTemplateId = existingUser.TimesheetTemplate ?? Utils.Constants.EIGHHOURSSHIFT // This line is temporary
        });
    }

    public IEnumerable<TimesheetControlApprovalDto> GetTimesheetControlRequests(DateTime startDate, DateTime endDate) =>
        _timesheetControlRepository.GetTimesheetControlRequests(startDate, endDate);

    public bool ApproveTimesheetsRequests(int[] ids, int loggedUserId, string loggedUserName)
    {
        int status = (int)ApprovalStatusOption.Approved;

        // get timesheets control requests
        var timesheetsRequests = _timesheetControlRepository.GetTimesheetsApprovalRecords(ids);

        // update timesheets control requests
        bool response = _timesheetControlRepository.ProcessTimesheetsRequests(ids, status);

        // update approvals if there are any
        int[] userIds = timesheetsRequests.Select(x => x.UserId).Distinct().ToArray();
        int[] timesheetControlIds = timesheetsRequests.Select(x => x.TimesheetPeriodId).Distinct().ToArray();
        var approvals = _approvalRepository.GetRegularTimeApprovals(userIds, timesheetControlIds);
        if (approvals.Any())
        {
            _approvalRepository.ProcessRequests(
                approvals.Select(x => x.ApprovalId).ToArray(),
                status);
        }

        // update timesheet records
        foreach (var item in timesheetsRequests)
        {
            _timesheetDataRepository.UpdateRegularRecords(
            item.UserId,
            item.TimesheetPeriod.Date,
            status);
        }

        // Create an approval history record per request
        foreach (var item in approvals)
        {
            CreateApprovalHistoryRecord(
                status,
                loggedUserId,
                loggedUserName,
                item.TimesheetControlId.Value,
                item.ApprovalId,
                ApprovalType.RegularTime
            );
        }

        return true;
    }

    public bool ReopenTimesheetsRequests(int[] ids, int loggedUserId, string loggedUserName)
    {
        int status = (int)ApprovalStatusOption.NotSubmitted;

        // get timesheets control requests
        var timesheetsRequests = _timesheetControlRepository.GetTimesheetsApprovalRecords(ids);

        // update timesheets control requests
        bool response = _timesheetControlRepository.ProcessTimesheetsRequests(ids, status);

        // update approvals if there are any
        int[] userIds = timesheetsRequests.Select(x => x.UserId).Distinct().ToArray();
        int[] timesheetControlIds = timesheetsRequests.Select(x => x.TimesheetPeriodId).Distinct().ToArray();
        var approvals = _approvalRepository.GetRegularTimeApprovals(userIds, timesheetControlIds);
        if (approvals.Any())
        {
            _approvalRepository.ProcessRequests(
                approvals.Select(x => x.ApprovalId).ToArray(),
                status);
        }

        // update timesheet records
        foreach (var item in timesheetsRequests)
        {
            _timesheetDataRepository.UpdateRegularRecords(
            item.UserId,
            item.TimesheetPeriod.Date,
            status);
        }

        // Create an approval history record per request
        foreach (var item in approvals)
        {
            CreateApprovalHistoryRecord(
                status,
                loggedUserId,
                loggedUserName,
                item.TimesheetControlId.Value,
                item.ApprovalId,
                ApprovalType.RegularTime
            );
        }

        return true;
    }

    public bool DeleteTimesheetsRequests(int[] ids)
    {
        //get timesheets control requests
        var timesheetsRequests = _timesheetControlRepository.GetTimesheetsApprovalRecords(ids);
        DateTime period = timesheetsRequests.First().TimesheetPeriod;
        int[] userIds = timesheetsRequests.Select(x => x.UserId).Distinct().ToArray();
        int[] timesheetControlIds = timesheetsRequests.Select(x => x.TimesheetPeriodId).Distinct().ToArray();

        //delete timesheets control records
        _timesheetControlRepository.ProcessTimesheetsRequests(ids, (int)ApprovalStatusOption.NotSubmitted);

        // delete timesheet rows, assuming all records are the same period
        _timesheetDataRepository.DeleteTimesheetOnlyRegularTime(period, userIds);

        // Delete approval records if there are any for the selected timesheets
        _approvalRepository.DeleteTimesheetRecordApproval(period, userIds);

        // Delete history approvals
        _approvalHistoryRepository.DeleteRegularApprovalHistoryRecords(userIds, timesheetControlIds);

        return true;
    }
}
