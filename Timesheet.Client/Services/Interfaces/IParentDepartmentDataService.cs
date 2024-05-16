using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces;

public interface IParentDepartmentDataService
{
    Task<ApiResponse<IEnumerable<ParentDepartment>>> GetParentDepartments();
}