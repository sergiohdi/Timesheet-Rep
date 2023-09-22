using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.EF_Implementations;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations
{
    public class ProjectBusiness : IProjectBusiness
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITimesheetDataRepository _timesheetDataRepository;

        public ProjectBusiness(IProjectRepository projectRepository, ITimesheetDataRepository timesheetDataRepository)
        {
            _projectRepository = projectRepository;
            _timesheetDataRepository = timesheetDataRepository;
        }

        public IEnumerable<ProjectDto> GetProjects(bool? isOpen)
        {
            return _projectRepository.GetProjects(isOpen);
        }

        public IEnumerable<ProjectLightDto> GetProjectsForDropDown()
        {
            return _projectRepository.GetProjectsForDropDown();
        }

        public ProjectDto GetProjectById(int projectId)
        {
            return _projectRepository.GetProjectById(projectId);
        }

        public bool CreateProject(ProjectDto project)
        {
            return _projectRepository.CreateProject(project);
        }

        public bool UpdateProject(ProjectDto project)
        {
            return _projectRepository.UpdateProject(project);
        }

        public bool UpdateProjectState(int projectId)
        {
            ProjectDto project = _projectRepository.GetProjectById(projectId);
            project.ClosedStatus = !project.ClosedStatus;

            return _projectRepository.UpdateProjectState(project);
        }
        public bool DeleteProject(int projectId)
        {
            // Validate if the project exists
            ProjectDto project = _projectRepository.GetProjectById(projectId);
            if (project is null)
            {
                return false;
            }

            // Validate if the project is used in any Timesheet
            bool isUsedProject = _timesheetDataRepository.ValidateTimesheetByProjectId(projectId);
            if (isUsedProject)
            {
                return false;
            }

            return _projectRepository.DeleteProject(project);
        }



    }
}
