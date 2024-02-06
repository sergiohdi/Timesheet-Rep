using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces
{
    public interface IProjectTeamUserDataService
    {
        Task<ApiResponse<IEnumerable<ProjectTeamUser>>> GetProjectTeamMembers(int projectId);

        Task<ApiResponse<bool>> AddProjectTeamMembers(int projectId, List<int> usersId);
    }
}