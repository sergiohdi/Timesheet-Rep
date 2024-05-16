using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.CustomFilters;
using Timesheet.Api.Extensions;
using Timesheet.Api.Models.DTOs;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Controllers;

[ApiController]
[Route("api/useractivitycode")]
public class UserActivityCodeController : ControllerBase
{
    private readonly IUserActivityCodeBusiness _userActivityCodeBusiness;
    private readonly ILogger<UserActivityCodeController> _logger;

    public UserActivityCodeController(IUserActivityCodeBusiness userActivityCodeBusiness, ILogger<UserActivityCodeController> logger)
    {
        _userActivityCodeBusiness = userActivityCodeBusiness;
        _logger = logger;
    }

    [HttpGet("{userId}")]
    [AuthorizeRoles((int)UserRole.Admin)]
    public IActionResult GetUserActivitiesByUserId(int userId)
    {
        if (!ModelState.IsValid || userId == 0) 
        {
            return BadRequest(ModelState);
        }

        try
        {
            return Ok(_userActivityCodeBusiness.GetUsersActivitiesByUserId(userId));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred getting user activities");
        }
    }

    [HttpGet("fordrop/{userId}")]
    public IActionResult GetUserActivitiesByUserIdForDropDown(int userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            userId = userId == 0 ? HttpContext.GetUserIdInt() : userId;
            return Ok(_userActivityCodeBusiness.GetUsersActivitiesByUserIdForDropDown(userId));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred getting user activities");
        }
    }

    [HttpPost("{userId}/activities")]
    [AuthorizeRoles((int)UserRole.Admin)]
    public IActionResult UpdateUserActivities([FromRoute] int userId, [FromBody] List<UserActivityCodeDto> activities)
    {
        try
        {
            return Ok(_userActivityCodeBusiness.UpdateUserActivities(activities, userId));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred creating user activity code");
        }
    }
}
