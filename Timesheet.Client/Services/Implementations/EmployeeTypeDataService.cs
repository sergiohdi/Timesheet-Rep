using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations;

public class EmployeeTypeDataService : IEmployeeTypeDataService
{
    private readonly BaseDataService _baseService;

    public EmployeeTypeDataService(BaseDataService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ApiResponse<IEnumerable<EmployeeType>>> GetEmployeeTypes()
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList("employeetype");
        return new ApiResponse<IEnumerable<EmployeeType>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<EmployeeType>>(data)
                : new List<EmployeeType>()
        };
    }
}
