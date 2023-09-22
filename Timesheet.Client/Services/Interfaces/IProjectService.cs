using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ApiResponse<bool>> CreateProject(Project project);
        Task<ApiResponse<bool>> DeleteProject(int projectId);
        Task<ApiResponse<Project>> GetProjectById(int projectId);
        Task<ApiResponse<IEnumerable<Project>>> GetProjects(bool? isOpen);
        Task<ApiResponse<IEnumerable<ProjectLight>>> GetProjectsForDropDown();
        Task<ApiResponse<bool>> UpdateProject(Project project);
    }
}