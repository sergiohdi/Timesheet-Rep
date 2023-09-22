using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces
{
    public interface IUserActivityCodeRepository
    {
        IEnumerable<UserActivityCodeDto> GetUsersActivitiesByUserId(int userId);

        IEnumerable<UserActivityCodeLightDto> GetUsersActivitiesByUserIdForDropDown(int userId);

        UserActivityCodeDto GetUserActivityCode(int userActivityCodeId);
        void RemoveUserActivities(int userId);

        bool InsertUserActivities(List<UserActivityCodeDto> activities);

    }
}
