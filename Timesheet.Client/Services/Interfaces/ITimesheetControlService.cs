using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces
{
    public interface ITimesheetControlService
    {
        Task<ApiResponse<IEnumerable<TimesheetControl>>> GetTimesheetControl();
        Task<ApiResponse<bool>> UpdateApprovalStatus(List<int> ids);
    }
}
