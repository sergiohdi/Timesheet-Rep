using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations
{
    public class UserActivityCodeBusiness : IUserActivityCodeBusiness
    {
        private readonly IUserActivityCodeRepository _userActivityCodeRepository;

        public UserActivityCodeBusiness(IUserActivityCodeRepository userActivityCodeRepository)
        {
            _userActivityCodeRepository = userActivityCodeRepository;
        }

        public IEnumerable<UserActivityCodeDto> GetUsersActivitiesByUserId(int userId)
        {
            return _userActivityCodeRepository.GetUsersActivitiesByUserId(userId);
        }

        public IEnumerable<UserActivityCodeLightDto> GetUsersActivitiesByUserIdForDropDown(int userId)
        {
            return _userActivityCodeRepository.GetUsersActivitiesByUserIdForDropDown(userId);
        }

        public UserActivityCodeDto GetUserActivityCode(int userActivityCodeId)
        {
            return _userActivityCodeRepository.GetUserActivityCode(userActivityCodeId);
        }
        public bool UpdateUserActivities(List<UserActivityCodeDto> activities, int userId)
        {
            _userActivityCodeRepository.RemoveUserActivities(userId);

            return _userActivityCodeRepository.InsertUserActivities(activities);
        }




    }
}
