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
    public class TimesheetTypeRepository : ITimesheetTypeRepository
    { 

        private readonly TimesheetContext _db;
        private readonly IMapper _mapper;

        public TimesheetTypeRepository(TimesheetContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IEnumerable<TimesheetTypeDto> GetTimesheetTypes()
        {
            return _mapper.Map<IEnumerable<TimesheetTypeDto>>(_db.TimesheetType);
        }
    }
}
