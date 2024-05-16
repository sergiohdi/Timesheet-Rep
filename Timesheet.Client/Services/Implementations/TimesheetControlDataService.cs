using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations;

public class TimesheetControlDataService : ITimesheetControlDataService
{
    private readonly BaseDataService _baseService;

    public TimesheetControlDataService(BaseDataService baseService)
    {
        _baseService = baseService;
    }   

    public async Task<ApiResponse<IEnumerable<TimesheetControl>>> GetTimesheetControl()
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList(Constants.timesheetControlEndpoint);
        return new ApiResponse<IEnumerable<TimesheetControl>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<TimesheetControl>>(data)
                : new List<TimesheetControl>()
        };
    }

    public async Task<ApiResponse<TimesheetControl>> GetTimesheetControlRecord(DateTime period, int userId = 0)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList($"{Constants.timesheetControlEndpoint}/{userId}?period={period.Date:yyyy-MM-dd}");
        return new ApiResponse<TimesheetControl>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && data != null
                ? JsonConvert.DeserializeObject<TimesheetControl>(data)
                : default
        };
    }

    public async Task<ApiResponse<bool>> UpdateApprovalStatus(List<int> ids)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Update(Constants.timesheetControlEndpoint, ids);
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<TimesheetControl>> CreateTimesheetControl(DateTime period)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Create(Constants.timesheetControlEndpoint, new { Period = period });
        return new ApiResponse<TimesheetControl>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && data != null 
                ? JsonConvert.DeserializeObject<TimesheetControl>(data)
                : null
        };
    }

    public async Task<ApiResponse<IEnumerable<TimesheetControlApproval>>> GetTimesheetControlRequests(DateTime startDate, DateTime endDate, bool isSupervisor = false)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Create($"{Constants.timesheetControlEndpoint}/timesheetcontrol", new { StartDate = startDate, EndDate = endDate, IsSupervisor = isSupervisor });
        return new ApiResponse<IEnumerable<TimesheetControlApproval>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<TimesheetControlApproval>>(data)
                : new List<TimesheetControlApproval>()
        };
    }
    public async Task<ApiResponse<bool>> ApproveTimesheetsRequests(List<int> ids)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Update($"{Constants.timesheetControlEndpoint}/timesheetcontrol/approve", ids.ToArray());
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<bool>> ReopenTimesheetsRequests(List<int> ids)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Update($"{Constants.timesheetControlEndpoint}/timesheetcontrol/reopen", ids.ToArray());
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<bool>> DeleteTimesheetsRequests(List<int> ids)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Delete($"{Constants.timesheetControlEndpoint}/timesheetcontrol/delete", ids.ToArray());
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }
}
