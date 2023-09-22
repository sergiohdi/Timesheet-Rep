using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations
{
    public class ApprovalService : IApprovalService
    {
        private readonly BaseService _baseService;

        public ApprovalService(BaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ApiResponse<bool>> CreateTimeoffRequest(TimeoffApproval request)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.Create(Constants.approvalEndpoint, request);
            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }

        public async Task<ApiResponse<IEnumerable<TimeoffApproval>>> GetTimeOffRecords(DateTime period)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.Create($"{Constants.approvalEndpoint}/timeoff",new { Period = period });
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

        public async Task<ApiResponse<IEnumerable<TimeoffApproval>>> GetTimeOffRequests(DateTime startDate, DateTime endDate, int userId = 0)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.Create($"{Constants.approvalEndpoint}/timeoff/filtered", new { StartDate = startDate, EndDate = endDate, UserId = userId });
            return new ApiResponse<IEnumerable<TimeoffApproval>>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<List<TimeoffApproval>>(data)
                    : new List<TimeoffApproval>()
            };
        }
    }
}
