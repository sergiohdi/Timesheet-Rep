using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations
{
    public class GeneralDataService : IGeneralDataService
    {
        private readonly BaseDataService _baseService;

        public GeneralDataService(BaseDataService baseService)
        {
            _baseService = baseService;
        }
        
        public async Task<ApiResponse<IEnumerable<General>>> GetGeneralValues(string group)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList("general", $"?group={group}");
            return new ApiResponse<IEnumerable<General>>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<List<General>>(data)
                    : new List<General>()
            };
        }

        public async Task<ApiResponse<IEnumerable<DateTime>>> GetWeekendDates()
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList("general/weekenddates");
            return new ApiResponse<IEnumerable<DateTime>>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<List<DateTime>>(data)
                    : new List<DateTime>()
            };
        }
    }
}
