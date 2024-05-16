using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations;

public class TimesheetTypeDataService : ITimesheetTypeDataService
{
    private readonly BaseDataService _baseService;

    public TimesheetTypeDataService(BaseDataService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ApiResponse<IEnumerable<TimesheetType>>> GetTimesheetTypes()
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList("timesheettype");           
        return new ApiResponse<IEnumerable<TimesheetType>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<TimesheetType>>(data)
                : new List<TimesheetType>()
        };
    }
}
