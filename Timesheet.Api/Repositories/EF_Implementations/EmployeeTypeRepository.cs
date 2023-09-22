using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Repositories.EF_Implementations
{
    public class EmployeeTypeRepository : IEmployeeTypeRepository
    {
        private readonly TimesheetContext _db;
        private readonly IMapper _mapper;

        public EmployeeTypeRepository(TimesheetContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IEnumerable<EmployeeTypeDto> GetEmployeeTypes()
        {
            return _mapper.Map<IEnumerable<EmployeeTypeDto>>(_db.EmployeeType);
        }

        public EmployeeTypeDto GetEmployeeTypeById(int employeeTypeId)
        {
            EmployeeType employeeType = _db.EmployeeType.FirstOrDefault(x => x.Emptypeid == employeeTypeId);
            return _mapper.Map<EmployeeTypeDto>(employeeType);
        }
    }
}
