using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations
{
    public class ParentDepartmentService : IParentDepartmentService
    {
        private readonly BaseService _baseService;

        public ParentDepartmentService(BaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ApiResponse<IEnumerable<ParentDepartment>>> GetParentDepartments()
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList("parentdepartment");
            return new ApiResponse<IEnumerable<ParentDepartment>>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<List<ParentDepartment>>(data)
                    : new List<ParentDepartment>()
            };
        }
    }
}