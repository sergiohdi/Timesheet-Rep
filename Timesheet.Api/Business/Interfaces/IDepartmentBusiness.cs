using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces;

public interface IDepartmentBusiness
{
    IEnumerable<DepartmentDto> GetDepartments(bool? DisabledSetting);
    DepartmentDto GetDepartmentById(int Id);
    bool CreateDepartment(DepartmentDto department);
    bool UpdateDepartment(DepartmentDto department);
    bool DeleteDepartment(int Id);
}
