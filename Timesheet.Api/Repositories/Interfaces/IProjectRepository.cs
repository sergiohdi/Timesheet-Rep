using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces;

public interface IProjectRepository
{
    IEnumerable<ProjectDto> GetProjects(bool? isOpen);
    IEnumerable<ProjectLightDto> GetProjectsForDropDown();
    ProjectDto GetProjectById(int projectId);
    bool CreateProject(ProjectDto project);
    bool UpdateProject(ProjectDto project);
    bool DeleteProject(ProjectDto project);
    bool UpdateProjectState(ProjectDto project);
    bool ValidateProjectsByClientId(int clientId);
}
