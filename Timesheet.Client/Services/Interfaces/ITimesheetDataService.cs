using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces;

public interface ITimesheetDataService
{
    Task<ApiResponse<IEnumerable<TimesheetItem>>> GetTimesheetDataByUserAsync(int userId, int year, int month, int period);
    Task<ApiResponse<Task>> UpdateTimesheetBaseProperties(int userId, UpdateTimesheetBasePropertiesRequest request);
    Task<ApiResponse<long>> UpdateTimesheetHours(int userId, TimesheetItem record);
    Task<ApiResponse<Task>> DeleteTimesheetRecord(int userId, TimesheetItem item);
}
