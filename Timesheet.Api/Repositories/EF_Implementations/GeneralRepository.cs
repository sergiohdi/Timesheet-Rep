using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Repositories.EF_Implementations;

public class GeneralRepository : IGeneralRepository
{
    private readonly TimesheetContext _db;
    private readonly IMapper _mapper;

    public GeneralRepository(TimesheetContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public IEnumerable<GeneralDto> GetGeneralRecords(string group)
    {
        IQueryable<General> result = _db.General.AsNoTracking();

        if (!string.IsNullOrEmpty(group))
        {
            result = result.Where(x => x.GralGroup.ToLower().Trim().Equals(group.ToLower().Trim()));
        }

        return _mapper.Map<IEnumerable<GeneralDto>>(result.ToList());
    }
}
