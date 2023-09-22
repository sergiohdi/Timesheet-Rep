using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces
{
    public interface ITimesheetControlRepository
    {
        IEnumerable<TimesheetControlDto> GetTimesheetControl();
        bool UpdateApprovalStatus(int[] ids);
        IEnumerable<TimesheetControlDto> GetTimesheetControlById(int[] ids);
    }
}
