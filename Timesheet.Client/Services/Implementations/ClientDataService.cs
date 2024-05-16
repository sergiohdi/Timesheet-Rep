using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations;

public class ClientDataService : IClientDataService
{
    private readonly BaseDataService _baseService;

    public ClientDataService(BaseDataService serviceClient)
    {
        _baseService = serviceClient;
    }

    public async Task<ApiResponse<IEnumerable<Models.Client>>> GetClients(bool? disabled)
    {
        string disabledString = string.Empty;
        if (disabled.HasValue) 
        {
            string boolValue = disabled.Value ? "true" : "false";
            disabledString = $"?disabled={boolValue}";
        }

        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList(Constants.clientEndpoint, disabledString);
        return new ApiResponse<IEnumerable<Models.Client>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<Models.Client>>(data)
                : new List<Models.Client>()
        };
    }

    public async Task<ApiResponse<IEnumerable<Models.ClientLight>>> GetClientsForDropDown()
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList($"{Constants.clientEndpoint}/fordrop");
        return new ApiResponse<IEnumerable<Models.ClientLight>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<Models.ClientLight>>(data)
                : new List<Models.ClientLight>()
        };
    }

    public async Task<ApiResponse<Models.Client>> GetClientById(int clientId) 
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetById(Constants.clientEndpoint, clientId);
        return new ApiResponse<Models.Client>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<Models.Client>(data)
                : new Models.Client()
        };
    }
    
    public async Task<ApiResponse<bool>> CreateClient(Models.Client client)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Create(Constants.clientEndpoint, client);
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }
    
    public async Task<ApiResponse<bool>> UpdateClient(Models.Client client)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Update(Constants.clientEndpoint, client);
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<bool>> DeleteClient(int clientId) 
    { 
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Delete(Constants.clientEndpoint, clientId);
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    } 
}
