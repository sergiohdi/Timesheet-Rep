using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserDto> GetUsers(bool? disabled);

        UserDto GetUser(int Id);

        bool CreateUser(UserDto user);

        bool UpdateUser(UserDto user);

        bool DeleteUser(UserDto user);

        bool ValidateUsersByDepartmentId(int departmentId);
    }
}
