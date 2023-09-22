using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Controllers
{
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

        [HttpPost]
        public IActionResult SaveTimeOff([FromBody] ApprovalDto approvalDto) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(_approvalBusiness.SaveTimeOffRequest(approvalDto));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred saving timeoff request");
            }
        }

        [HttpPost("timeoff")]
        public IActionResult GetTimeOffRecords([FromBody] JsonElement element)
        {
            try
            {
                var asd = JsonConvert.DeserializeObject<Dictionary<string,string>>(element.ToString());
                DateTime.TryParse(asd["period"], out DateTime period);
                return Ok(_approvalBusiness.GetTimeOffRecords(period));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred fetching timeoff records");
            }
        }

        [HttpPut("timeoff/{timeoffId}")]
        public IActionResult UpdateTimeoffRecord([FromRoute] int timeoffId, ApprovalDto approvalDto) 
        {
            try
            {
                return Ok(_approvalBusiness.UpdateTimeoffRecord(approvalDto));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred fetching timeoff records");
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
                return Ok(_approvalBusiness.DeleteTimeoffRecords(timeoffId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred deleting timeoff record");
            }
        }

        [HttpPost("timeoff/filtered")]
        public IActionResult GetTimeoffRequests([FromBody] JsonElement element)
        {
            try
            {
                var paramsObj = JsonConvert.DeserializeObject<Dictionary<string,string>>(element.ToString());
                DateTime.TryParse(paramsObj["startDate"], out DateTime startDate);
                DateTime.TryParse(paramsObj["endDate"], out DateTime endDate);
                int.TryParse(paramsObj["userId"], out int userId);
                return Ok(_approvalBusiness.GetTimeOffRequests(startDate, endDate, userId));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred fetching timeoff records");
            }
            
        }
    }
}
