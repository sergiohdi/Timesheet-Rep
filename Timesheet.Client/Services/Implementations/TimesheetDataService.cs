using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations;

public class TimesheetDataService : ITimesheetDataService
{
    private readonly BaseDataService _baseService;

    public TimesheetDataService(BaseDataService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ApiResponse<IEnumerable<TimesheetItem>>> GetTimesheetDataByUserAsync(int userId, int year, int month, int period)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList(Constants.timesheetEndpoint, $"?userId={userId}&year={year}&month={month}&period={period}");
        return new ApiResponse<IEnumerable<TimesheetItem>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<TimesheetItem>>(data)
                : new List<TimesheetItem>()
        };
    }

    public async Task<ApiResponse<Task>> UpdateTimesheetBaseProperties(UpdateTimesheetBasePropertiesRequest request)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Update(Constants.timesheetEndpoint, request);
        return new ApiResponse<Task>
        {
            Status = status,
            Errors = errors
        };
    }

    public async Task<ApiResponse<long>> UpdateTimesheetHours(int userId, TimesheetItem record)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Update($"{Constants.timesheetEndpoint}/{userId}", record);
        return new ApiResponse<long>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? Convert.ToInt64(data)
                : 0
        };
    }

    public async Task<ApiResponse<Task>> DeleteTimesheetRecord(int userId, TimesheetItem item)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Delete($"{Constants.timesheetEndpoint}/{userId}", item);
        return new ApiResponse<Task>
        {
            Status = status,
            Errors = errors
        };
    }
}
