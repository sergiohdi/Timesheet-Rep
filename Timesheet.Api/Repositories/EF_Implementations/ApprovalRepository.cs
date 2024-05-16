using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Repositories.EF_Implementations;

public class ApprovalRepository : IApprovalRepository
{
    private readonly TimesheetContext _db;
    private readonly IMapper _mapper;

    public ApprovalRepository(TimesheetContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public int SaveApprovalRequest(ApprovalDto approvalRequest)
    {
        Approvals approval = _mapper.Map<Approvals>(approvalRequest);
        _db.Approvals.Add(approval);
        _db.SaveChanges();

        return approval.ApprovalId;
    }

    public IEnumerable<ApprovalDto> GetTimeOffRecords(DateTime period, int userId)
    {
        IEnumerable<Approvals> timeOffList = _db.Approvals
            .AsNoTracking()
            .Where(x => x.Period == period && x.UserId == userId && x.TimeOffId != null)
            .ToList();
        return _mapper.Map<IEnumerable<ApprovalDto>>(timeOffList);
    }

    public ApprovalDto GetTimeoffById(int timeoffApprovalId, int userId)
    {
        Approvals timeoff = _db.Approvals
            .AsNoTracking()
            .FirstOrDefault(x => x.ApprovalId == timeoffApprovalId && x.UserId == userId);

        return _mapper.Map<ApprovalDto>(timeoff);
    }

    public IEnumerable<ApprovalDto> GetRegularTimeApprovals(int[] userIds, int[] timesheetControlIds)
    {
        return _db.Approvals.AsNoTracking()
                            .Where(x => x.ApprovalType == (int)ApprovalType.RegularTime &&
                                                        userIds.Contains(x.UserId) &&
                                                        timesheetControlIds.Contains(x.TimesheetControlId.Value))
                            .ToList()
                            .Select(x => _mapper.Map<ApprovalDto>(x));
    }

    public bool DeleteTimeoffRecord(ApprovalDto timeoff)
    {
        _db.Remove(_mapper.Map<Approvals>(timeoff));
        return _db.SaveChanges() > 0;
    }

    public bool UpdateTimeoffRecord(ApprovalDto approval)
    {
        Approvals record = _mapper.Map<Approvals>(approval);
        _db.Update(record);
        return _db.SaveChanges() > 0;
    }

    public bool UpdateApprovalStatus(List<DateTime> periods, List<int> userIds)
    {
        _db.Approvals.Where(x => userIds.Contains(x.UserId) && periods.Contains(x.Period.Value) && x.ApprovalType == 1)
                     .ExecuteUpdate(s => s.SetProperty(x => x.ApprovalStatusId, 2));

        return true;
    }

    public IEnumerable<ApprovalTimeoffRequestDto> GetTimeoffRequests(DateTime startDate, DateTime endDate, int supervisorId)
    {
        IEnumerable<ApprovalTimeoffRequestDto> timeoffRequests = (from a in _db.Approvals
                                                                  join u in _db.User on a.UserId equals u.Id
                                                                  join asi in _db.ApprovalStatus on a.ApprovalStatusId equals asi.Approvalstatusid
                                                                  join to in _db.TimeOff on a.TimeOffId equals to.TimeOffId
                                                                  where a.ApprovalType == (int)ApprovalType.Timeoff &&
                                                                        ((a.StartDate.Date >= startDate.Date && a.StartDate <= endDate.Date) ||
                                                                        (a.EndDate.Date >= startDate.Date && a.EndDate.Date <= endDate.Date))
                                                                  select new ApprovalTimeoffRequestDto
                                                                  {
                                                                      ApprovalId = a.ApprovalId,
                                                                      UserId = a.UserId,
                                                                      ApprovalStatusId = a.ApprovalStatusId,
                                                                      TimeOffId = a.TimeOffId,
                                                                      ApprovalType = a.ApprovalType,
                                                                      StartDate = a.StartDate,
                                                                      EndDate = a.EndDate,
                                                                      Duration = a.Duration,
                                                                      Comments = a.Comments,
                                                                      Period = a.Period,
                                                                      UserName = $"{u.LastName}, {u.FirstName} ({u.LoginName})",
                                                                      StatusName = asi.Appstatusname,
                                                                      TimeoffName = to.TimeOffName,
                                                                      SupervisorId = u.SupervisorId ?? 0,
                                                                      TimesheetControlId = a.TimesheetControlId,
                                                                  }).OrderBy(x => x.StartDate).ToList();

        if (supervisorId > 0)
        {
            timeoffRequests = timeoffRequests.Where(x => x.SupervisorId == supervisorId && x.ApprovalStatusId == (int)ApprovalStatusOption.Waiting).ToList();
        }

        return timeoffRequests;
    }

    public IEnumerable<ApprovalRegularTimeRequestsDto> GetRegularTimeRequests(DateTime startDate, DateTime endDate, int supervisorId)
    {
        List<ApprovalRegularTimeRequestsDto> regularTimeRequests = (from a in _db.Approvals
                                                           join u in _db.User on a.UserId equals u.Id
                                                           join asi in _db.ApprovalStatus on a.ApprovalStatusId equals asi.Approvalstatusid
                                                           where a.ApprovalType == (int)ApprovalType.RegularTime &&
                                                                 a.ApprovalStatusId == (int)ApprovalStatusOption.Waiting &&
                                                                 u.SupervisorId == supervisorId &&
                                                                 ((a.StartDate.Date >= startDate.Date && a.StartDate <= endDate.Date) ||
                                                                 (a.EndDate.Date >= startDate.Date && a.EndDate.Date <= endDate.Date))
                                                           select new ApprovalRegularTimeRequestsDto
                                                           {
                                                               ApprovalId = a.ApprovalId,
                                                               UserId = a.UserId,
                                                               ApprovalStatusId = a.ApprovalStatusId,
                                                               TimeOffId = a.TimeOffId,
                                                               ApprovalType = a.ApprovalType,
                                                               StartDate = a.StartDate,
                                                               EndDate = a.EndDate,
                                                               Duration = a.Duration,
                                                               Comments = a.Comments,
                                                               Period = a.Period,
                                                               UserName = $"{u.LastName}, {u.FirstName} ({u.LoginName})",
                                                               StatusName = asi.Appstatusname,
                                                               SupervisorId = u.SupervisorId ?? 0,
                                                               TimesheetControlId = a.TimesheetControlId
                                                           }).ToList();

        regularTimeRequests.ForEach(x => x.TimesheetPeriod = $"{x.StartDate:MMM dd, yyyy} - {x.EndDate:MMM dd, yyyy}");

        return regularTimeRequests;
    }

    public bool ProcessRequests(int[] ids, int status)
    {
        _db.Approvals.Where(x => ids.Contains(x.ApprovalId))
                     .ExecuteUpdate(s => s.SetProperty(x => x.ApprovalStatusId, status));

        return true;
    }

    public IEnumerable<ApprovalDto> GetApprovalRecords(int[] ids)
    {
        IEnumerable<Approvals> timeOffList = _db.Approvals.AsNoTracking()
                                                          .Where(x => ids.Contains(x.ApprovalId)).ToList();
        return _mapper.Map<IEnumerable<ApprovalDto>>(timeOffList);
    }

    public bool DeleteApprovalRequests(int[] ids)
    {
        _db.Approvals.Where(x => ids.Contains(x.ApprovalId))
                     .ExecuteDelete();
        return true;
    }

    public bool DeleteTimesheetRecordApproval(DateTime period, int[] userIds)
    {
        _db.Approvals.Where(x => 
            x.Period == period &&
            x.ApprovalType == (int)ApprovalType.RegularTime &&
            userIds.Contains(x.UserId))
            .ExecuteDelete();

        return true;
    }

    public List<ApprovalDto> GetFutureApprovals(DateTime currentPeriod, int userId) 
    {
        List<Approvals> result = _db.Approvals
            .AsNoTracking()
            .Where( x => x.UserId == userId && x.Period > currentPeriod && x.Duration > 0)
            .ToList();

        return _mapper.Map<List<ApprovalDto>>(result);
    }

    public bool UpdateApprovals(IEnumerable<ApprovalDto> approvals)
    {
        _db.UpdateRange(_mapper.Map<IEnumerable<Approvals>>(approvals));
        return _db.SaveChanges() > 0;
    }

    public bool ApprovalActionRegularTimeRecords(int[] ids, ApprovalStatusOption approvalStatus, int userId, string userName)
    {
        // get approval records filtered by id
        IEnumerable<ApprovalDto> approvals = GetApprovalRecords(ids);
        if (!approvals.Any()) 
        {
            return false;
        }

        string sqlBaseStatement = Api.Utils.Constants.BASETRYCATCHSQLSTATEMENT;
        StringBuilder sb = new StringBuilder();
        ApprovalDto approval = approvals.First();
        DateTime startDate = approval.StartDate; 
        DateTime endDate = approval.EndDate;
        int counter = 0;
        IEnumerable<CreateApprovalRequestDto> approvalRequest = approvals.Select(a => new CreateApprovalRequestDto
        {
            ActionDate = DateTime.Now,
            ActionType = (int)approvalStatus,
            IdUser = userId,
            UserName = userName,
            IdTimesheetControl = a.TimesheetControlId,
            ApprovalId = a.ApprovalId,
            TimesheetType = (int)ApprovalType.RegularTime
        });

        // update status property per each approval record
        sb.Append($@"

                UPDATE [dbo].[Approvals]
                SET [ApprovalStatusId] = {(int)approvalStatus}
                WHERE [ApprovalId] IN ({ string.Join(",", ids) })

            ");

        // update status property per each timesheet control
        sb.Append($@"

                UPDATE [dbo].[TimesheetControl]
                SET [ApprovalStatusId] = {(int)approvalStatus}
                WHERE [TimesheetPeriodId] IN ({ string.Join(", ", approvals.Select(x => x.TimesheetControlId)) })

            ");

        if (approvalStatus != ApprovalStatusOption.SupervisorApproval)
        {
            // update timesheetdata records
            sb.Append($@"

                    UPDATE [dbo].[TimesheetData]
                    SET [ApprovalStatus] = {(int)approvalStatus}
                    WHERE tasktimeoffid IS NULL AND approvalstatus = 1 AND userid in ({string.Join(", ", approvals.Select(x => x.UserId).Distinct())}) AND entrydate >= '{startDate.Date.ToString("yyyy-MM-dd")}' AND entrydate <= '{endDate.Date.ToString("yyyy-MM-dd")}'

                ");
        }

        // create an approval history record per each approval record
        StringBuilder approvalStringBuilder = new StringBuilder();
        approvalStringBuilder.Append(@"

                INSERT INTO [dbo].[ApprovalHistory]
            ");
        foreach (var item in approvalRequest)
        {
            string statement = counter == 0
                ? $"VALUES ('{item.ActionDate:yyyy-MM-dd HH:mm:ss}', {item.ActionType}, {item.IdUser}, {item.IdTimesheetControl}, {item.TimesheetType}, '{item.Comments}', '{item.UserName}', {item.ApprovalId})"
                : $"('{item.ActionDate:yyyy-MM-dd HH:mm:ss}', {item.ActionType}, {item.IdUser}, {item.IdTimesheetControl}, {item.TimesheetType}, '{item.Comments}', '{item.UserName}', {item.ApprovalId})";

            approvalStringBuilder.Append(statement);
        }
        sb.Append(approvalStringBuilder);
        sb.Append(' ');

        sqlBaseStatement =  sqlBaseStatement.Replace("{0}", sb.ToString());

        int result = _db.Database.SqlQueryRaw<int>(sqlBaseStatement).ToList().FirstOrDefault();
        return result == 1;
    }
}
