using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Extensions;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Repositories.EF_Implementations
{
    public class TimesheetControlRepository : ITimesheetControlRepository
    {
        private readonly TimesheetContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public TimesheetControlRepository(TimesheetContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public IEnumerable<TimesheetControlDto> GetTimesheetControl()
        {
            IQueryable<TimesheetControl> result = _db.TimesheetControl.AsNoTracking();
            return _mapper.Map<IEnumerable<TimesheetControlDto>>(result.ToList());
        }

        public bool UpdateApprovalStatus(int[] ids)
        {
            _db.TimesheetControl.Where(x => ids.Contains(x.TimesheetPeriodId))
                .ExecuteUpdate(s => s.SetProperty(x => x.ApprovalStatusId, 2));

            return true;
        }

        public IEnumerable<TimesheetControlDto> GetTimesheetControlById(int[] ids)
        {
            IEnumerable<TimesheetControl> result = _db.TimesheetControl.AsNoTracking()
                                                                       .Where(x => ids.Contains(x.TimesheetPeriodId))
                                                                       .ToList();
            return _mapper.Map<IEnumerable<TimesheetControlDto>>(result);
        }

    }
}
