using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Implementations;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

public class UserActivityCodeDataService : IUserActivityCodeDataService
{

    private readonly BaseDataService _baseService;

    public UserActivityCodeDataService(BaseDataService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ApiResponse<IEnumerable<UserActivityCode>>> GetUsersActivitiesByUserId(int userId)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList(Constants.userActivityCodeEndpoint, $"?userId={userId}");

        return new ApiResponse<IEnumerable<UserActivityCode>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<UserActivityCode>>(data)
                : new List<UserActivityCode>()
        };
    }

    public async Task<ApiResponse<IEnumerable<UserActivityCode>>> GetUsersActivitiesByUserIdForDropDown(int userId)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList(Constants.userActivityCodeEndpoint, $"?userId={userId}");

        return new ApiResponse<IEnumerable<UserActivityCode>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<UserActivityCode>>(data)
                : new List<UserActivityCode>()
        };
    }

    public async Task<ApiResponse<bool>> UpdateUserActivities(List<UserActivityCode> activities, int userId)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Create($"{Constants.userActivityCodeEndpoint}/{userId}/activities", activities);

        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }
}
