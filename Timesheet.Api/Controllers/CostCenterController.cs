using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.CustomFilters;
using Timesheet.Api.Models.DTOs;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Controllers;

[ApiController]
[Route("api/costcenter")]
[AuthorizeRoles((int)UserRole.Admin)]
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
    [ProducesResponseType(typeof(IEnumerable<CostCenterDto>), 200)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)]
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
    [ProducesResponseType(typeof(CostCenterDto), 200)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)]
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
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)]
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
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)]
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
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)]
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
