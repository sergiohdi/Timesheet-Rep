using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces
{
    public interface IEmployeeTypeService
    {
        Task<ApiResponse<IEnumerable<EmployeeType>>> GetEmployeeTypes();
    }
}