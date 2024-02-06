using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations
{
    public class TimeOffDataService : ITimeOffDataService
    {
        private readonly BaseDataService _baseService;

        public TimeOffDataService(BaseDataService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ApiResponse<IEnumerable<TimeOff>>> GetTimeOffs()
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList("timeoff");
            return new ApiResponse<IEnumerable<TimeOff>>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<List<TimeOff>>(data)
                    : new List<TimeOff>()
            };
        }
    }
}
