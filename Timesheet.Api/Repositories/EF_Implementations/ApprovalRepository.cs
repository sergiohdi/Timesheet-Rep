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

        public bool SaveApprovalRequest(ApprovalDto approvalRequest)
        {
            Approvals approval = _mapper.Map<Approvals>(approvalRequest);
            _db.Approvals.Add(approval);
            return _db.SaveChanges() > 0;
        }

        public IEnumerable<ApprovalDto> GetTimeOffRecords(DateTime period)
        {
            IEnumerable<Approvals> timeOffList = _db.Approvals.AsNoTracking().Where(x => x.Period == period && x.ApprovalStatusId != (int)ApprovalStatusOption.Rejected).ToList();
            return _mapper.Map<IEnumerable<ApprovalDto>>(timeOffList);
        }

        public ApprovalDto GetTimeoffById(int id)
        {
            Approvals timeoff = _db.Approvals.AsNoTracking().FirstOrDefault(x => x.ApprovalId == id);
            return _mapper.Map<ApprovalDto>(timeoff);
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
            _db.Approvals.Where(x => userIds.Contains(x.UserId) && periods.Contains(x.Period.Value))
                         .ExecuteUpdate(s => s.SetProperty(x => x.ApprovalStatusId, 2));

            return true;
        }

        public IEnumerable<ApprovalDto> GetTimeoffRequests(DateTime startDate, DateTime endDate, int supervisorId = 0)
        {
            IQueryable<Approvals> timeOffList = _db.Approvals.AsNoTracking()
                                                             .Where(x =>
                                                                    x.ApprovalType == 2 &&
                                                                    (startDate.Date >= x.StartDate && startDate.Date <= x.EndDate) ||
                                                                    (endDate >= x.StartDate && endDate <= x.EndDate));

            if (supervisorId > 0)
            {
                timeOffList = timeOffList.Join(_db.User.AsNoTracking(),
                                                                  a => a.UserId,
                                                                  u => u.Id,
                                                                  (a, u) => new { a, u })
                                         .Where(x => x.u.SupervisorId == supervisorId)
                                         .Select(x => x.a);
            }


            return _mapper.Map<IEnumerable<ApprovalDto>>(timeOffList.ToList());
        }
    }
}
