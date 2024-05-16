using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces;

public interface IEmployeeTypeRepository
{
    IEnumerable<EmployeeTypeDto> GetEmployeeTypes();
    EmployeeTypeDto GetEmployeeTypeById(int employeeTypeId);
}
