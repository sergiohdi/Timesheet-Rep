using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces
{
    public interface ITimesheetControlBusiness
    {
        IEnumerable<TimesheetControlDto> GetTimesheetControl();
        bool UpdateApprovalStatus(int[] ids);
    }
}
