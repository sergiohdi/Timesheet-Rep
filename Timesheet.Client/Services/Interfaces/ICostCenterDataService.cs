using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces
{
    public interface ICostCenterDataService
    {
        Task<ApiResponse<bool>> CreateCostCenter(CostCenter costCenter);
        Task<ApiResponse<bool>> DeleteCostCenter(int costCenterId);
        Task<ApiResponse<CostCenter>> GetCostCenterById(int costCenterId);
        Task<ApiResponse<IEnumerable<CostCenter>>> GetCostCenters(bool? disabled);
        Task<ApiResponse<bool>> UpdateCostCenter(CostCenter costCenter);
    }
}