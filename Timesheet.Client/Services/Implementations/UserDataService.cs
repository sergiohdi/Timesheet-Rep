using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations
{
    public class UserDataService : IUserDataService
    {
        private readonly BaseDataService _baseService;

        public UserDataService(BaseDataService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ApiResponse<IEnumerable<User>>> GetUsers(bool? disabled)
        {

            string disabledString = string.Empty;
            if (disabled.HasValue)
            {
                string boolValue = disabled.Value ? "true" : "false";
                disabledString = $"?disabled={boolValue}";
            }

            //(ResponseStatus status, string data, List<string> errors) = await _baseService.GetList(Constants.userEndpoint, $"?disabled={disabled}");

            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList(Constants.userEndpoint, disabledString);
            return new ApiResponse<IEnumerable<User>>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<List<User>>(data)
                    : new List<User>()
            };
        }

        public async Task<ApiResponse<User>> GetUserById(int userId) 
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetById(Constants.userEndpoint, userId);
            return new ApiResponse<User>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<User>(data)
                    : new User()
            };
        }

        public async Task<ApiResponse<bool>> CreateUser(User user)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.Create(Constants.userEndpoint, user);
            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }

        public async Task<ApiResponse<bool>> UpdateUser(User user)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.Update(Constants.userEndpoint, user);
            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }

        public async Task<ApiResponse<bool>> DeleteUser(int UserId)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.Delete(Constants.userEndpoint, UserId);
            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }

        public async Task<ApiResponse<IEnumerable<UserActivityCode>>> GetActivitiesByUser(int userId)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetById(Constants.userActivityCodeEndpoint, userId);
            return new ApiResponse<IEnumerable<UserActivityCode>>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success ? JsonConvert.DeserializeObject<IEnumerable<UserActivityCode>>(data) : null
            };
        }

        public async Task<ApiResponse<IEnumerable<UserActivityCodeLight>>> GetActivitiesByUserForDropDown(int userId)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetById($"{Constants.userActivityCodeEndpoint}/fordrop", userId);
            return new ApiResponse<IEnumerable<UserActivityCodeLight>>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success ?
                    JsonConvert.DeserializeObject<IEnumerable<UserActivityCodeLight>>(data) :
                    new List<UserActivityCodeLight>()
            };
        }

        public async Task<ApiResponse<IEnumerable<Substitute>>> GetSubstitutesForUser(int userId)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList($"{Constants.userEndpoint}/{userId}/substitutes");

            return new ApiResponse<IEnumerable<Substitute>>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<List<Substitute>>(data)
                    : new List<Substitute>()
            };
        }

        public async Task<ApiResponse<bool>> UpdateSubstitutes(List<Substitute> substitutes, int userId)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.Create($"{Constants.userEndpoint}/{userId}/substitutes", substitutes);

            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }
    }
}
