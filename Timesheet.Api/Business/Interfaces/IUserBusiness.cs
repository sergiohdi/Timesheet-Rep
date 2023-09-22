using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces
{
    public interface IUserBusiness
    {
        IEnumerable<UserDto> GetUsers(bool? disabled);

        UserDto GetUser(int userId);

        bool CreateUser(UserDto user);

        bool UpdateUser(UserDto user);

        bool DeleteUser(int userId);
    }
}
