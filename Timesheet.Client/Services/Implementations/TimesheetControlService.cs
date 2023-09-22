using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;
namespace Timesheet.Client.Services.Implementations
{
    public class TimesheetControlService : ITimesheetControlService
    {
        private readonly BaseService _baseService;

        public TimesheetControlService(BaseService baseService)
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
        public async Task<ApiResponse<bool>> UpdateApprovalStatus(List<int> ids)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.Update(Constants.timesheetControlEndpoint, ids);
            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<bool>(data)
                    : false
            };
        }
    }
}
