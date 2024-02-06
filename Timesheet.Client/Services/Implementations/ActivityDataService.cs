using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations
{
    public class ActivityDataService : IActivityDataService
    {
        private readonly BaseDataService _baseService;

        public ActivityDataService(BaseDataService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ApiResponse<IEnumerable<Activity>>> GetActivities(bool? disabled)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList(Constants.activityEndpoint, $"?disabled={disabled}");
            return new ApiResponse<IEnumerable<Activity>>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<List<Activity>>(data)
                    : new List<Activity>()
            };
        }

        public async Task<ApiResponse<IEnumerable<Activity>>> GetActivitiesForUser()
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList(Constants.activityEndpoint, $"/forusers");
            return new ApiResponse<IEnumerable<Activity>>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<List<Activity>>(data)
                    : new List<Activity>()
            };
        }

        public async Task<ApiResponse<Activity>> GetActivityById(int activityId)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetById(Constants.activityEndpoint, activityId);
            return new ApiResponse<Activity>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<Activity>(data)
                    : new Activity()
            };
        }

        public async Task<ApiResponse<bool>> CreateActivity(Activity activity)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.Create(Constants.activityEndpoint, activity);
            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)      
            };
        }
        
        public async Task<ApiResponse<bool>> UpdateActivity(Activity activity)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.Update(Constants.activityEndpoint, activity);
            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }

      
        public async Task<ApiResponse<bool>> DeleteActivity(int activityId)
        { 
            (ResponseStatus status, string data, List<string> errors) = await _baseService.Delete(Constants.activityEndpoint, activityId);
            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        } 
    }
}
