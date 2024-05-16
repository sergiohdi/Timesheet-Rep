using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces;

public interface IUserRepository
{
    IEnumerable<UserDto> GetUsers(bool? disabled);
    UserDto GetUser(int Id);
    bool CreateUser(UserDto user, string password);
    bool UpdateUser(UserDto user);
    bool DeleteUser(UserDto user);
    bool ValidateUsersByDepartmentId(int departmentId);
    UserDto GetUserByEmail(string email);
    bool UpdatePassword(UserDto user);
}

