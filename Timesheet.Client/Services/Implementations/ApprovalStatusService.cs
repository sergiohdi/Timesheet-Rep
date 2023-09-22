using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations
{
    public class ApprovalStatusService : IApprovalStatusService
    {
        private readonly BaseService _baseService;

        public ApprovalStatusService(BaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ApiResponse<IEnumerable<ApprovalStatus>>> GetApprovalStatuses()
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList("approvalstatus");
            return new ApiResponse<IEnumerable<ApprovalStatus>>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<IEnumerable<ApprovalStatus>>(data)
                    : new List<ApprovalStatus>()
            };
        }
    }
}
