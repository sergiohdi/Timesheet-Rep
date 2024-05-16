using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces;

public interface IProjectHasUserRepository
{
    IEnumerable<ProjectTeamUserDTO> GetUsersByProject(int projectId);
    bool AddUsersToTeamProject(int projectId, IEnumerable<int> usersId);
    void DeleteTeamProjectUsers(int projectId);
}

