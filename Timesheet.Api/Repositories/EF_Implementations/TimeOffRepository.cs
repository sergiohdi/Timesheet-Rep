using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Repositories.EF_Implementations;

public class TimeOffRepository : ITimeOffRepository
{
    private readonly TimesheetContext _db;
    private readonly IMapper _mapper;

    public TimeOffRepository(TimesheetContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public IEnumerable<TimeOffDto> GetTimeOffList() => 
        _mapper.Map<IEnumerable<TimeOffDto>>(_db.TimeOff.OrderBy(x => x.TimeOffCode).ToList());

    public TimeOffDto GetTimeOffById(int timeOffId) => 
        _mapper.Map<TimeOffDto>(_db.TimeOff.AsNoTracking().FirstOrDefault(x => x.TimeOffId == timeOffId));
}
