using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces;

public interface IUserActivityCodeBusiness
{
    IEnumerable<UserActivityCodeDto> GetUsersActivitiesByUserId(int userId);
    IEnumerable<UserActivityCodeLightDto> GetUsersActivitiesByUserIdForDropDown(int userId); 
    UserActivityCodeDto GetUserActivityCode(int userActivityCodeId);
    bool UpdateUserActivities(List<UserActivityCodeDto> activities, int userId);
}
