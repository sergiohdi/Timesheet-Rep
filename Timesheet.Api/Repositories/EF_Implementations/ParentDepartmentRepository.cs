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
    public class ParentDepartmentRepository : IParentDepartmentRepository
    {
        private readonly TimesheetContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ParentDepartmentRepository(TimesheetContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public IEnumerable<ParentDepartmentDto> GetParentDepartments()
        {
            IQueryable<ParentDepartment> result = _db.ParentDepartment.AsNoTracking().OrderBy(x => x.Name);


            return _mapper.Map<IEnumerable<ParentDepartmentDto>>(result.ToList());
        }
    }
}
