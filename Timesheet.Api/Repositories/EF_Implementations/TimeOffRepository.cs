using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Repositories.EF_Implementations
{
    public class TimeOffRepository : ITimeOffRepository
    {
        private readonly TimesheetContext _db;
        private readonly IMapper _mapper;

        public TimeOffRepository(TimesheetContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IEnumerable<TimeOffDto> GetTimeOffList()
        {
            return _mapper.Map<IEnumerable<TimeOffDto>>(_db.TimeOff.AsNoTracking().OrderBy(x => x.TimeOffName).ToList());
        }

        public TimeOffDto GetTimeOffById(int timeOffId)
        {
            return _mapper.Map<TimeOffDto>(_db.TimeOff.AsNoTracking().FirstOrDefault(x => x.TimeOffId == timeOffId));
        }
    }
}
