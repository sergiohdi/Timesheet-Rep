using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations;

public class CostCenterBusiness : ICostCenterBusiness
{
    private readonly ICostCenterRepository _costCenterRepository;

    public CostCenterBusiness(ICostCenterRepository costCenterRepository) => _costCenterRepository = costCenterRepository;

    public IEnumerable<CostCenterDto> GetCostCenters(bool? disabled) => _costCenterRepository.GetCostCenters(disabled);

    public CostCenterDto GetCostCenterById(int costCenterId) => _costCenterRepository.GetCostCenterById(costCenterId);

    public bool CreateCostCenter(CostCenterDto costCenter) => _costCenterRepository.CreateCostCenter(costCenter);

    public bool UpdateCostCenter(CostCenterDto costCenter) => _costCenterRepository.UpdateCostCenter(costCenter);

    public bool UpdateCostCenterState(int costCenterId)
    {
        CostCenterDto costCenter = _costCenterRepository.GetCostCenterById(costCenterId);
        costCenter.Disabled = !costCenter.Disabled;

        return _costCenterRepository.UpdateCostCenterState(costCenter);
    }
}
