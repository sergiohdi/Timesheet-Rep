using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces;

public interface IDepartmentDataService
{
    Task<ApiResponse<bool>> CreateDepartment(Department department);
    Task<ApiResponse<bool>> DeleteDepartment(int Id);
    Task<ApiResponse<Department>> GetDepartmentById(int Id);
    Task<ApiResponse<IEnumerable<Department>>> GetDepartments(bool? DisabledSetting);
    Task<ApiResponse<bool>> UpdateDepartment(Department department);
}