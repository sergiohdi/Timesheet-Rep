using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces
{
    public interface IActivityService
    {
        Task<ApiResponse<bool>> CreateActivity(Activity activity);
        Task<ApiResponse<bool>> UpdateActivity(Activity activity);
        Task<ApiResponse<bool>> DeleteActivity(int activityId);
        Task<ApiResponse<IEnumerable<Activity>>> GetActivities(bool? disabled);
        Task<ApiResponse<IEnumerable<Activity>>> GetActivitiesForUser();
        Task<ApiResponse<Activity>> GetActivityById(int activityId);
    }
}