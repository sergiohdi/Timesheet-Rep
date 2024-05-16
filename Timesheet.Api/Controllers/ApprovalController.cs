using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.CustomFilters;
using Timesheet.Api.Extensions;
using Timesheet.Api.Models.DTOs;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Controllers;

[ApiController]
[Route("api/approval")]
public class ApprovalController : ControllerBase
{
    private readonly IApprovalBusiness _approvalBusiness;
    private readonly ILogger<ActivityController> _logger;

    public ApprovalController(IApprovalBusiness approvalBusiness, ILogger<ActivityController> logger)
    {
        _approvalBusiness = approvalBusiness;
        _logger = logger;
    }

    [HttpPost("createtimeoff")]
    public IActionResult CreateTimeoffRequest([FromBody] ApprovalDto approvalDto) 
    {
        if (!ModelState.IsValid) 
        {
            return BadRequest(ModelState);
        }

        try
        {
            int loggedUserId = HttpContext.GetUserIdInt();
            approvalDto.UserId = loggedUserId;
            string loggedUserName = HttpContext.GetUserNameLog();
            return Ok(_approvalBusiness.CreateTimeoffRequest(approvalDto, loggedUserId, loggedUserName));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred saving time off request");
        }
    }

    [HttpPost("createregulartime")]
    [AuthorizeRoles((int)UserRole.Admin, (int)UserRole.WTSAdmin)]
    public IActionResult CreateRegularTimeRequest([FromBody] ApprovalDto approvalDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            int loggedUserId = HttpContext.GetUserIdInt();
            string loggedUserName = HttpContext.GetUserNameLog();
            return Ok(_approvalBusiness.CreateRegularTimeRequest(approvalDto, loggedUserId, loggedUserName));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred saving regular time request");
        }
    }

    [HttpPost("timeoff/timesheet")]
    public IActionResult GetTimeOffRecords([FromBody] JsonElement element)
    {
        try
        {
            var jsonObject = JsonConvert.DeserializeObject<Dictionary<string,string>>(element.ToString());
            DateTime.TryParse(jsonObject["period"], out DateTime period);
            Int32.TryParse(jsonObject["period"], out int userId);
            userId = userId == 0 ? HttpContext.GetUserIdInt() : userId;

            return Ok(_approvalBusiness.GetTimeOffRecords(period, userId));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred fetching time off records");
        }
    }

    [HttpPut("timeoff/{timeoffId}")]
    public IActionResult UpdateTimeoffRecord([FromRoute] int timeoffId, ApprovalDto approvalDto) 
    {
        try
        {
            int loggedUserId = HttpContext.GetUserIdInt();
            approvalDto.UserId = loggedUserId;
            string loggedUserName = HttpContext.GetUserNameLog();
            return Ok(_approvalBusiness.UpdateTimeoffRecord(approvalDto, loggedUserId, loggedUserName));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred fetching time off records");
        }
    }

    [HttpDelete("timeoff/{timeoffId}")]
    public IActionResult DeleteTimeoffRecord([FromRoute] int timeoffId) 
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            int userId = HttpContext.GetUserIdInt();
            return Ok(_approvalBusiness.DeleteTimeoffRecords(timeoffId, userId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred deleting the time off record");
        }
    }

    [HttpPost("timeoffrequests")]
    [AuthorizeRoles((int)UserRole.Admin, (int)UserRole.WTSAdmin)]
    public IActionResult GetTimeoffRequests([FromBody] JsonElement element)
    {
        try
        {
            int loggedUserId = HttpContext.GetUserIdInt(); 
            var paramsObj = JsonConvert.DeserializeObject<Dictionary<string,string>>(element.ToString());
            DateTime.TryParse(paramsObj["startDate"], out DateTime startDate);
            DateTime.TryParse(paramsObj["endDate"], out DateTime endDate);
            
            return Ok(_approvalBusiness.GetTimeOffRequests(startDate, endDate, loggedUserId));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred fetching timeoff records");
        }
        
    }

    [HttpPost("regulartime")]
    [AuthorizeRoles((int)UserRole.Admin, (int)UserRole.WTSAdmin)]
    public IActionResult GetRegularTimeRequests([FromBody] JsonElement element)
    {
        try
        {
            int loggedUserId = HttpContext.GetUserIdInt();
            var paramsObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(element.ToString());
            DateTime.TryParse(paramsObj["startDate"], out DateTime startDate);
            DateTime.TryParse(paramsObj["endDate"], out DateTime endDate);

            return Ok(_approvalBusiness.GetRegularTimeRequests(startDate, endDate, loggedUserId));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred fetching timesheet records");
        }

    }

    [HttpPut("regulartime/approve")]
    [AuthorizeRoles((int)UserRole.Admin, (int)UserRole.WTSAdmin)]
    public IActionResult ApproveRegularTimeRequests([FromBody] int[] ids)
    {
        try
        {
            int loggedUserId = HttpContext.GetUserIdInt();
            string loggedUserName = HttpContext.GetUserNameLog();
            return Ok(_approvalBusiness.ApproveRegularTimeRequests(ids, loggedUserId, loggedUserName));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred approving timeoff records");
        }
    }

    [HttpPut("regulartime/reject")]
    [AuthorizeRoles((int)UserRole.Admin, (int)UserRole.WTSAdmin)]
    public IActionResult RejectRegularTimeRequests([FromBody] int[] ids)
    {
        try
        {
            int loggedUserId = HttpContext.GetUserIdInt();
            string loggedUserName = HttpContext.GetUserNameLog();
            return Ok(_approvalBusiness.RejectRegularTimeRequests(ids, loggedUserId, loggedUserName));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred approving timeoff records");
        }
    }

    [HttpPut("timeoff/approve")]
    [AuthorizeRoles((int)UserRole.Admin, (int)UserRole.WTSAdmin)]
    public IActionResult ApproveTimeoffRequests([FromBody] int[] ids)
    {
        try
        {
            int loggedUserId = HttpContext.GetUserIdInt();
            string loggedUserName = HttpContext.GetUserNameLog();
            return Ok(_approvalBusiness.ApproveTimeoffRequests(ids, loggedUserId, loggedUserName));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred approving timeoff records");
        }
    }

    // Todo: check if this endpoint is useless
    [HttpPut("timeoff/reopen")]
    [AuthorizeRoles((int)UserRole.Admin, (int)UserRole.WTSAdmin)]
    public IActionResult ReopenTimeoffRequests([FromBody] int[] ids)
    {
        try
        {
            int loggedUserId = HttpContext.GetUserIdInt();
            string loggedUserName = HttpContext.GetUserNameLog();
            return Ok(_approvalBusiness.ReopenTimeoffRequests(ids, loggedUserId, loggedUserName));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred reopening timeoff records");
        }
    }

    [HttpPut("timeoff/reject")]
    [AuthorizeRoles((int)UserRole.Admin, (int)UserRole.WTSAdmin)]
    public IActionResult RejectTimeoffSelected([FromBody] int[] ids)
    {
        try
        {
            int loggedUserId = HttpContext.GetUserIdInt();
            string loggedUserName = HttpContext.GetUserNameLog();
            return Ok(_approvalBusiness.RejectTimeoffSelected(ids, loggedUserId, loggedUserName));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred rejecting timeoff records");
        }
    }

    [HttpPost("timeoff/delete")]
    [AuthorizeRoles((int)UserRole.Admin, (int)UserRole.WTSAdmin)]
    public IActionResult DeleteTimeoffRequests([FromBody] int[] ids)
    {
        try
        {
            return Ok(_approvalBusiness.DeleteTimeoffRequests(ids));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error has occurred deleting timeoff records");
        }
    }
}
