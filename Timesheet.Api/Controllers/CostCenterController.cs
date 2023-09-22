using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Controllers
{
    [ApiController]
    [Route("api/costcenter")]
    public class CostCenterController : ControllerBase
    {
        private readonly ICostCenterBusiness _costCenterBusiness;
        private readonly ILogger<CostCenterController> _logger;

        public CostCenterController(ICostCenterBusiness costCenterBusiness, ILogger<CostCenterController> logger)
        {
            _costCenterBusiness = costCenterBusiness;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetCostCenters(bool? disabled)
        {
            try
            {
                return Ok(_costCenterBusiness.GetCostCenters(disabled));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred getting cost centers");
            }
        }

        [HttpGet("{costCenterId}")]
        public IActionResult GetCostCenterById(int costCenterId)
        {
            try
            {
                return Ok(_costCenterBusiness.GetCostCenterById(costCenterId));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred getting cost center");
            }
        }

        [HttpPost]
        public IActionResult CreateCostCenter(CostCenterDto costCenter)
        {
            try
            {
                return Ok(_costCenterBusiness.CreateCostCenter(costCenter));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred creating cost center");
            }
        }

        [HttpPut]
        public IActionResult UpdateCostCenter(CostCenterDto costCenter)
        {
            try
            {
                return Ok(_costCenterBusiness.UpdateCostCenter(costCenter));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred updating cost center");
            }
        }

        [HttpDelete("{costCenterId}")]
        public IActionResult ChangeCostCenterStatus(int costCenterId)
        {
            try
            {
                return Ok(_costCenterBusiness.UpdateCostCenterState(costCenterId));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred deleting cost center");
            }
        }

    }
}
