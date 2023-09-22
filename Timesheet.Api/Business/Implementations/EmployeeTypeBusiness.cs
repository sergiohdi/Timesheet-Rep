using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations
{
    public class EmployeeTypeBusiness : IEmployeeTypeBusiness
    {
        private readonly IEmployeeTypeRepository _employeeTypeRepository;

        public EmployeeTypeBusiness(IEmployeeTypeRepository employeeTypeRepository )
        {
            _employeeTypeRepository = employeeTypeRepository;
        }

        public IEnumerable<EmployeeTypeDto> GetEmployeeTypes()
        {
            return _employeeTypeRepository.GetEmployeeTypes();
        }
    }
}
