using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations;

public class ProjectHasUserBusiness : IProjectHasUserBusiness
{
    private readonly IProjectHasUserRepository _projectHasUserRepository;

    public ProjectHasUserBusiness(IProjectHasUserRepository projectHasUserRepository) => _projectHasUserRepository = projectHasUserRepository;

    public bool AddUsersToTeamProject(int projectId, IEnumerable<int> usersId)
    {
        _projectHasUserRepository.DeleteTeamProjectUsers(projectId);
        if (usersId.Contains(0))
        {
            return true;
        }

        return _projectHasUserRepository.AddUsersToTeamProject(projectId, usersId);
    }

    public IEnumerable<ProjectTeamUserDTO> GetUsersByProject(int ProjectId) => _projectHasUserRepository.GetUsersByProject(ProjectId);
}
