using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces
{
    public interface IUserActivityCodeDataService
    {
        Task<ApiResponse<IEnumerable<UserActivityCode>>> GetUsersActivitiesByUserId(int userId);
        Task<ApiResponse<IEnumerable<UserActivityCode>>> GetUsersActivitiesByUserIdForDropDown(int userId);
        Task<ApiResponse<bool>> UpdateUserActivities(List<UserActivityCode> activities, int userId);
    }
}
