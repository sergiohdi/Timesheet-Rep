using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.CustomFilters;
using Timesheet.Api.Extensions;
using Timesheet.Api.Utils;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Controllers;

[ApiController]
[Route("api/timesheetcontrol")]
public class TimesheetControlController : ControllerBase
{
    private readonly ITimesheetControlBusiness _timesheetControlBusiness;
    private readonly ILogger<TimesheetControlController> _logger;

    public TimesheetControlController(ITimesheetControlBusiness timesheetControlBusiness, ILogger<TimesheetControlController> logger)
    {
        _timesheetControlBusiness = timesheetControlBusiness;
        _logger = logger;
    }

    [HttpGet]
    [AuthorizeRoles((int)UserRole.Admin, (int)UserRole.WTSAdmin)]
    public IActionResult GetTimesheetControl()
    {
        try
        {
            return Ok(_timesheetControlBusiness.GetTimesheetControl());
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred getting Timesheet Control");
        }
    }

    [HttpGet("{userId}")]
    public IActionResult GetTimesheetControlRecord([FromQuery] string period, [FromRoute] int userId = 0)
    {
        try
        {
            bool validRole = new UserRoleValidation(userId, HttpContext.GetUserRoleInt())
                .IsValidUserId()
                .IsNotAdminOrWtsRole()
                .IsValidRole();

            if (!validRole)
            {
                return Forbid();
            }

            DateTime.TryParse(period, out DateTime datePeriod);
            userId = userId == 0
                ? Convert.ToInt32(HttpContext.GetUserId())
                : userId;
            return Ok(_timesheetControlBusiness.GetTimesheetControlRecord(datePeriod, userId));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred getting Timesheet Control record");
        }
    }

    [HttpPut]
    [AuthorizeRoles((int)UserRole.Admin, (int)UserRole.WTSAdmin)]
    public IActionResult UpdateApprovalStatus([FromBody]int[] timesheetIds)
    {
        try
        {
            return Ok(_timesheetControlBusiness.UpdateApprovalStatus(timesheetIds));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred updating Timesheet Control");
        }
    }

    [HttpPost]
    public IActionResult CreateTimesheetControlRecord([FromBody] JsonElement element)
    {
        try
        {
            Dictionary<string, string> jsonObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(element.ToString());
            DateTime.TryParse(jsonObject["period"], out DateTime period);
            int userId = HttpContext.GetUserIdInt();

            return Ok(_timesheetControlBusiness.CreateTimesheetControlRecord(period, userId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred creating a Timesheet Control");
        }
    }

    [HttpPost("TimesheetControl")]
    [AuthorizeRoles((int)UserRole.Admin, (int)UserRole.WTSAdmin)]
    public IActionResult GetTimesheetControlRequests([FromBody] JsonElement element)
    {
        try
        {
            var paramsObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(element.ToString());
            DateTime.TryParse(paramsObj["startDate"], out DateTime startDate);
            DateTime.TryParse(paramsObj["endDate"], out DateTime endDate);

            return Ok(_timesheetControlBusiness.GetTimesheetControlRequests(startDate, endDate));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred fetching timesheetcontrol records");
        }

    }

    [HttpPut("timesheetcontrol/approve")]
    [AuthorizeRoles((int)UserRole.Admin, (int)UserRole.WTSAdmin)]
    public IActionResult ApproveTimesheetsRequests([FromBody] int[] ids)
    {
        try
        {
            int loggedUserId = HttpContext.GetUserIdInt();
            string loggedUserName = HttpContext.GetUserNameLog();
            return Ok(_timesheetControlBusiness.ApproveTimesheetsRequests(ids, loggedUserId, loggedUserName));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred approving timesheets records");
        }
    }

    [HttpPut("timesheetcontrol/reopen")]
    [AuthorizeRoles((int)UserRole.Admin, (int)UserRole.WTSAdmin)]
    public IActionResult ReopenTimesheetsRequests([FromBody] int[] ids)
    {
        try
        {
            int loggedUserId = HttpContext.GetUserIdInt();
            string loggedUserName = HttpContext.GetUserNameLog();
            return Ok(_timesheetControlBusiness.ReopenTimesheetsRequests(ids, loggedUserId, loggedUserName));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred reopening timesheets records");
        }
    }

    [HttpPost("timesheetcontrol/delete")]
    [AuthorizeRoles((int)UserRole.Admin, (int)UserRole.WTSAdmin)]
    public IActionResult DeleteTimesheetsRequests([FromBody] int[] ids)
    {
        try
        {
            return Ok(_timesheetControlBusiness.DeleteTimesheetsRequests(ids));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred deleting timesheets records");
        }
    }
}
