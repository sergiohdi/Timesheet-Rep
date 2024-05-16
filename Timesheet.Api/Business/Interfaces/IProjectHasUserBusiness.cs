using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces;

public interface IProjectHasUserBusiness
{
    IEnumerable<ProjectTeamUserDTO> GetUsersByProject(int ProjectId);
    bool AddUsersToTeamProject(int projectId, IEnumerable<int> usersId);
}
