using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations
{
    public class ApprovalHistoryService : IApprovalHistoryService
    {
        private readonly BaseDataService _baseService;

        public ApprovalHistoryService(BaseDataService baseDataService)
        {
            _baseService = baseDataService;
        }

        public async Task<ApiResponse<List<GetApprovalsHistory>>> GetApprovalsHistory(int timesheetId)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList($"{Constants.approvalHistoryEndPoint}/{timesheetId}");
            return new ApiResponse<List<GetApprovalsHistory>>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<List<GetApprovalsHistory>>(data)
                    : new List<GetApprovalsHistory>()
            };
        }

        public async Task<ApiResponse<bool>> CreateTimeoffRequest(CreateApprovalHistory request)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.Create($"{Constants.approvalHistoryEndPoint}", request);
            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }
    }
}
