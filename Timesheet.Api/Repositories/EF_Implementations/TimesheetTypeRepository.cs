using AutoMapper;
using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Repositories.EF_Implementations;

public class TimesheetTypeRepository : ITimesheetTypeRepository
{ 

    private readonly TimesheetContext _db;
    private readonly IMapper _mapper;

    public TimesheetTypeRepository(TimesheetContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public IEnumerable<TimesheetTypeDto> GetTimesheetTypes() => _mapper.Map<IEnumerable<TimesheetTypeDto>>(_db.TimesheetType);
}
