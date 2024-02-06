using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;
        private readonly ITimesheetDataRepository _timesheetDataRepository;

        public UserBusiness(IUserRepository userRepository, ITimesheetDataRepository timesheetDataRepository)
        {
            _userRepository = userRepository;
            _timesheetDataRepository = timesheetDataRepository;
        }

        public IEnumerable<UserDto> GetUsers(bool? disabled)
        {
            return _userRepository.GetUsers(disabled);
        }

        public UserDto GetUser(int userId)
        {
            return _userRepository.GetUser(userId);
        }

        public bool CreateUser(UserDto user)
        {
            return _userRepository.CreateUser(user);
        }

        public bool UpdateUser(UserDto user)
        {
            return _userRepository.UpdateUser(user);
        }

        public bool DeleteUser(int userId)
        {
            UserDto user = _userRepository.GetUser(userId);
            if (user is null) 
            {
                return false;
            }

            bool userExistsOnTimesheetRecords = _timesheetDataRepository.ValidateTimesheetByUserId(userId);
            if (userExistsOnTimesheetRecords)
            {
                return false;
            }

            bool result;
            try
            {
                result = _userRepository.DeleteUser(user);
            }
            catch (System.Exception ex)
            {
                if (!ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE"))
                {
                    throw;
                }

                result = false;
            }

            return result;
        }
    }
}
