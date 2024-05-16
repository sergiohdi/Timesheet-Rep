using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations;

public class ProjectTeamUserDataService : IProjectTeamUserDataService
{

    private readonly BaseDataService _baseService;

    public ProjectTeamUserDataService(BaseDataService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ApiResponse<bool>> AddProjectTeamMembers(int projectId, List<int> usersId)
    {
        if (!usersId.Any()) { 
            usersId.Add(0);
        }
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Create($"{Constants.ProjectHasUserEndpoint}/{projectId}", usersId);

        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
        };
    }

    public async Task<ApiResponse<IEnumerable<ProjectTeamUser>>> GetProjectTeamMembers(int projectId)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList($"{Constants.ProjectHasUserEndpoint}/{projectId}");

        return new ApiResponse<IEnumerable<ProjectTeamUser>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<ProjectTeamUser>>(data)
                : new List<ProjectTeamUser>()
        };
    }
}

