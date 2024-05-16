using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations;

public class CostCenterDataService : ICostCenterDataService
{
    private readonly BaseDataService _baseService;

    public CostCenterDataService(BaseDataService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ApiResponse<IEnumerable<CostCenter>>> GetCostCenters(bool? disabled)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList(Constants.costCenterEndpoint, $"?disabled={disabled}");
        return new ApiResponse<IEnumerable<CostCenter>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<CostCenter>>(data)
                : new List<CostCenter>()
        };
    }

    public async Task<ApiResponse<CostCenter>> GetCostCenterById(int costCenterId)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetById(Constants.costCenterEndpoint, costCenterId);
        return new ApiResponse<CostCenter>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<CostCenter>(data)
                : new CostCenter()
        };
    }

    public async Task<ApiResponse<bool>> CreateCostCenter(CostCenter costCenter)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Create(Constants.costCenterEndpoint, costCenter);
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<bool>> UpdateCostCenter(CostCenter costCenter)
    {
        await _baseService.Update(Constants.costCenterEndpoint, costCenter);
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Create(Constants.costCenterEndpoint, costCenter);
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<bool>> DeleteCostCenter(int costCenterId)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Delete(Constants.costCenterEndpoint, costCenterId);
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }
}
