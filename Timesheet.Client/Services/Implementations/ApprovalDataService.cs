using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations;

public class ApprovalDataService : IApprovalDataService
{
    private readonly BaseDataService _baseService;

    public ApprovalDataService(BaseDataService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ApiResponse<bool>> CreateTimeoffRequest(TimeoffApproval request)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Create($"{Constants.approvalEndpoint}/createtimeoff", request);
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<bool>> CreateRegularTimeRequest(RegularTimeApprovalRequest request)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Create($"{Constants.approvalEndpoint}/createregulartime", request);
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<IEnumerable<TimeoffApproval>>> GetTimeOffRecords(DateTime period)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Create($"{Constants.approvalEndpoint}/timeoff/timesheet",new { Period = period });
        return new ApiResponse<IEnumerable<TimeoffApproval>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<TimeoffApproval>>(data)
                : new List<TimeoffApproval>()
        };
    }

    public async Task<ApiResponse<bool>> UpdateTimeoffRecord(int timeoffId, TimeoffApproval record)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Update($"{Constants.approvalEndpoint}/timeoff/{timeoffId}", record);
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<bool>> DeleteTimeoffRecord(int requestId)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Delete($"{Constants.approvalEndpoint}/timeoff/{requestId}");
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<IEnumerable<TimeoffRequest>>> GetTimeOffRequests(DateTime startDate, DateTime endDate, bool isSupervisor)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Create($"{Constants.approvalEndpoint}/timeoffrequests", new { StartDate = startDate, EndDate = endDate, IsSupervisor = isSupervisor });
        return new ApiResponse<IEnumerable<TimeoffRequest>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<TimeoffRequest>>(data)
                : new List<TimeoffRequest>()
        };
    }

    public async Task<ApiResponse<IEnumerable<RegularTimeApproval>>> GetRegularTimeRequests(DateTime startDate, DateTime endDate)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Create($"{Constants.approvalEndpoint}/regulartime", new { StartDate = startDate, EndDate = endDate });
        return new ApiResponse<IEnumerable<RegularTimeApproval>>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success
                ? JsonConvert.DeserializeObject<List<RegularTimeApproval>>(data)
                : new List<RegularTimeApproval>()
        };
    }

    public async Task<ApiResponse<bool>> ApproveTimeoffRequests(List<int> ids)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Update($"{Constants.approvalEndpoint}/timeoff/approve", ids.ToArray());
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<bool>> ReopenTimeoffRequests(List<int> ids)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Update($"{Constants.approvalEndpoint}/timeoff/reopen", ids.ToArray());
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<bool>> RejectTimeoffSelected(List<int> ids)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Update($"{Constants.approvalEndpoint}/timeoff/reject", ids.ToArray());
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<bool>> ApproveRegularRequests(List<int> ids)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Update($"{Constants.approvalEndpoint}/regulartime/approve", ids.ToArray());
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<bool>> RejectRegularRequests(List<int> ids)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Update($"{Constants.approvalEndpoint}/regulartime/reject", ids.ToArray());
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }

    public async Task<ApiResponse<bool>> DeleteTimeoffRequests(List<int> ids)
    {
        (ResponseStatus status, string data, List<string> errors) = await _baseService.Delete($"{Constants.approvalEndpoint}/timeoff/delete", ids.ToArray());
        return new ApiResponse<bool>
        {
            Status = status,
            Errors = errors,
            Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
        };
    }
}
