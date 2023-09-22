using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces
{
    public interface ICostCenterRepository
    {
        IEnumerable<CostCenterDto> GetCostCenters(bool? disabled);

        CostCenterDto GetCostCenterById(int costCenterId);

        bool CreateCostCenter(CostCenterDto costCenter);

        bool UpdateCostCenter(CostCenterDto costCenter);

        bool UpdateCostCenterState(CostCenterDto costCenter);
    }
}
