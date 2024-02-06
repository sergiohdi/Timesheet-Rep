using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces
{
    public interface IUserDataService
    {
        Task<ApiResponse<bool>> CreateUser(User user);
        Task<ApiResponse<bool>> DeleteUser(int userId);
        Task<ApiResponse<User>> GetUserById(int userId);
        Task<ApiResponse<IEnumerable<User>>> GetUsers(bool? disabled);
        Task<ApiResponse<bool>> UpdateUser(User user);
        Task<ApiResponse<IEnumerable<UserActivityCode>>> GetActivitiesByUser(int userId);
        Task<ApiResponse<IEnumerable<UserActivityCodeLight>>> GetActivitiesByUserForDropDown(int userId);
        Task<ApiResponse<IEnumerable<Substitute>>> GetSubstitutesForUser(int userId);
        Task<ApiResponse<bool>> UpdateSubstitutes(List<Substitute> substitutes, int userId);
    }
}