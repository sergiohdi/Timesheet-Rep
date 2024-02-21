using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Repositories.EF_Implementations
{
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

        public IEnumerable<ApprovalDto> GetTimeOffRecords(DateTime period)
        {
            IEnumerable<Approvals> timeOffList = _db.Approvals
                .AsNoTracking()
                .Where(x => x.Period == period && x.TimeOffId != null)
                .ToList();
            return _mapper.Map<IEnumerable<ApprovalDto>>(timeOffList);
        }

        public ApprovalDto GetTimeoffById(int id)
        {
            Approvals timeoff = _db.Approvals.AsNoTracking().FirstOrDefault(x => x.ApprovalId == id);
            return _mapper.Map<ApprovalDto>(timeoff);
        }

        public IEnumerable<ApprovalDto> GetRegularTimeApprovals(int userId, DateTime period)
        {
            return _db.Approvals.AsNoTracking().Where(x => x.UserId == userId &&
                                                           x.Period == period.Date && 
                                                           x.ApprovalType == (int)ApprovalType.RegularTime).ToList()
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
                                                                          SupervisorId = u.SupervisorId ?? 0
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
                                                                   SupervisorId = u.SupervisorId ?? 0
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
            IEnumerable<Approvals> timeOffList = _db.Approvals.AsNoTracking().Where(x => ids.Contains(x.ApprovalId)).ToList();
            return _mapper.Map<IEnumerable<ApprovalDto>>(timeOffList);
        }

        public bool DeleteApprovalRequests(int[] ids)
        {
            _db.Approvals.Where(x => ids.Contains(x.ApprovalId))
                         .ExecuteDelete();
            return true;
        }

        public bool DeleteTimesheetRecordApproval(DateTime period, int userId)
        {
            _db.Approvals.Where(x => x.Period == period && x.UserId == userId && x.ApprovalType == 1)
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
    }
}
