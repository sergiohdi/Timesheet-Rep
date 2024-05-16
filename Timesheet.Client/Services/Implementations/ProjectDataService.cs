using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations;

public class ProjectDataService : IProjectDataService
{
    private readonly BaseDataService _baseService;
    
    public ProjectDataService(BaseDataService serviceClient)
    {
        _baseService = serviceClient;
    }

    public async Task<ApiResponse<IEnumerable<Project>>> GetProjects(bool? isOpen) 
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList(Constants.projectEndpoint, $"?isOpen={isOpen}");
        return new ApiResponse<IEnumerable<Models.Project>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<Project>>(data)
                : new List<Project>()
        };
    }

    public async Task<ApiResponse<IEnumerable<ProjectLight>>> GetProjectsForDropDown()
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList($"{Constants.projectEndpoint}/fordrop");
        return new ApiResponse<IEnumerable<Models.ProjectLight>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<ProjectLight>>(data)
                : new List<ProjectLight>()
        };
    }

    public async Task<ApiResponse<Project>> GetProjectById(int projectId)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetById(Constants.projectEndpoint, projectId);
        return new ApiResponse<Project>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<Project>(data)
                : new Project()
        };
    }

    public async Task<ApiResponse<bool>> CreateProject(Project project)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Create(Constants.projectEndpoint, project);
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<bool>> UpdateProject(Project project)
    { 
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Update(Constants.projectEndpoint, project);
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<bool>> DeleteProject(int projectId)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Delete(Constants.projectEndpoint, projectId);
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }
}
