using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.CustomFilters;
using Timesheet.Api.Models.DTOs;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Controllers;

[ApiController]
[Route("api/activity")]
[AuthorizeRoles((int)UserRole.Admin)]
public class ActivityController : ControllerBase
{
    private readonly IActivityBusiness _activityBusiness;
    private readonly ILogger<ActivityController> _logger;

    public ActivityController(IActivityBusiness activityBusiness, ILogger<ActivityController> logger)
    {
        _activityBusiness = activityBusiness;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ActivityDto>), 200)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public IActionResult GetActivities(bool? disabled) 
    {
        try
        {
            return Ok(_activityBusiness.GetActivities(disabled));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred getting activities");
        }
    }

    [HttpGet("forusers")]
    [ProducesResponseType(typeof(IEnumerable<ActivityDto>), 200)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public IActionResult GetActivitiesForUser()
    {
        try
        {
            return Ok(_activityBusiness.GetActivitiesForUser());
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred getting activities");
        }
    }

    [HttpGet("{activityId}")]
    [ProducesResponseType(typeof(ActivityDto), 200)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public IActionResult GetActivityById(int activityId)
    {
        try
        {
            return Ok(_activityBusiness.GetActivityById(activityId));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred getting activity");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(void), 400)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public IActionResult CreateActivity(ActivityDto activity)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_activityBusiness.CreateActivity(activity));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred creating activity");
        }
    }

    [HttpPut]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public IActionResult UpdateActivity(ActivityDto activity)
    {
        try
        {
            return Ok(_activityBusiness.UpdateActivity(activity));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred updating activity");
        }
    }

    [HttpDelete("{activityId}")]
    [AuthorizeRoles((int)UserRole.Admin)]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public IActionResult DeleteActivity(int activityId)
    {
        try
        {
            return Ok(_activityBusiness.DeleteActivity(activityId));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred deleting activity");
        }
    }
}
