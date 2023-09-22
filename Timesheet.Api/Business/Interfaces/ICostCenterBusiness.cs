using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces
{
    public interface ICostCenterBusiness
    {
        IEnumerable<CostCenterDto> GetCostCenters(bool? disabled);

        CostCenterDto GetCostCenterById(int costCenterId);

        bool CreateCostCenter(CostCenterDto costCenter);

        bool UpdateCostCenter(CostCenterDto costCenter);

        bool UpdateCostCenterState(int costCenterId);
    }
}
