using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations
{
    public class DepartmentDataService : IDepartmentDataService
    {
        private readonly BaseDataService _baseService;

        public DepartmentDataService(BaseDataService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ApiResponse<IEnumerable<Department>>> GetDepartments(bool? DisabledSetting)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetList(Constants.departmentEndpoint, $"?disabled={DisabledSetting}");
            return new ApiResponse<IEnumerable<Department>>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<List<Department>>(data)
                    : new List<Department>()
            };
        }

        public async Task<ApiResponse<Department>> GetDepartmentById(int Id)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.GetById(Constants.departmentEndpoint, Id);
            return new ApiResponse<Department>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<Department>(data)
                    : new Department()
            };
        }

        public async Task<ApiResponse<bool>> CreateDepartment(Department department)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.Create(Constants.departmentEndpoint, department);
            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }

        public async Task<ApiResponse<bool>> UpdateDepartment(Department department)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.Update(Constants.departmentEndpoint, department);
            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }

        public async Task<ApiResponse<bool>> DeleteDepartment(int Id)
        {
            (ResponseStatus status, string data, List<string> errors) = await _baseService.Delete(Constants.departmentEndpoint, Id);
            return new ApiResponse<bool>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success && JsonConvert.DeserializeObject<bool>(data)
            };
        }
    }
}
