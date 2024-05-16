using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces;

public interface IProjectBusiness
{
    IEnumerable<ProjectDto> GetProjects(bool? isOpen);
    IEnumerable<ProjectLightDto> GetProjectsForDropDown();
    ProjectDto GetProjectById(int projectId);
    bool CreateProject(ProjectDto project);
    bool UpdateProject(ProjectDto project);
    bool UpdateProjectState(int projectId);
    bool DeleteProject(int projectId);
}
