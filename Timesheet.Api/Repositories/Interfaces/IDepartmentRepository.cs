using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces;

public interface IDepartmentRepository
{
    IEnumerable<DepartmentDto> GetDepartments(bool? disabled);
    DepartmentDto GetDepartmentById(int departmentId);
    bool CreateDepartment(DepartmentDto department);
    bool UpdateDepartment(DepartmentDto department);
    bool DeleteDepartment(DepartmentDto department);
}
