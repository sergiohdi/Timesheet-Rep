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

        [HttpPost("createtimeoff")]
        public IActionResult CreateTimeoffRequest([FromBody] ApprovalDto approvalDto) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(_approvalBusiness.CreateTimeoffRequest(approvalDto));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred saving timeoff request");
            }
        }

        [HttpPost("createregulartime")]
        public IActionResult CreateRegularTimeRequest([FromBody] ApprovalDto approvalDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                const int userId = 579;
                return Ok(_approvalBusiness.CreateRegularTimeRequest(approvalDto, userId));
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

        [HttpPost("timeoff")]
        public IActionResult GetTimeoffRequests([FromBody] JsonElement element)
        {
            try
            {
                int userId = 0; 
                var paramsObj = JsonConvert.DeserializeObject<Dictionary<string,string>>(element.ToString());
                DateTime.TryParse(paramsObj["startDate"], out DateTime startDate);
                DateTime.TryParse(paramsObj["endDate"], out DateTime endDate);
                bool.TryParse(paramsObj["isSupervisor"], out bool isSupervisor);
                if (isSupervisor)
                {
                    userId = 561; // Get from token
                }
                return Ok(_approvalBusiness.GetTimeOffRequests(startDate, endDate, userId));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred fetching timeoff records");
            }
            
        }

        [HttpPost("regulartime")]
        public IActionResult GetRegularTimeRequests([FromBody] JsonElement element)
        {
            try
            {
                int userId = 561; // Get from token
                var paramsObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(element.ToString());
                DateTime.TryParse(paramsObj["startDate"], out DateTime startDate);
                DateTime.TryParse(paramsObj["endDate"], out DateTime endDate);
                return Ok(_approvalBusiness.GetRegularTimeRequests(startDate, endDate, userId));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred fetching timesheet records");
            }

        }

        [HttpPut("timeoff/approve")]
        public IActionResult ApproveTimeoffRequests([FromBody] int[] ids)
        {
            try
            {
                return Ok(_approvalBusiness.ApproveTimeoffRequests(ids));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred approving timeoff records");
            }
        }

        [HttpPut("timeoff/reopen")]
        public IActionResult ReopenTimeoffRequests([FromBody] int[] ids)
        {
            try
            {
                return Ok(_approvalBusiness.ReopenTimeoffRequests(ids));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred reopening timeoff records");
            }
        }

        [HttpPut("timeoff/rejectselect")]
        public IActionResult RejectTimeoffSelected([FromBody] int[] ids)
        {
            try
            {
                return Ok(_approvalBusiness.RejectTimeoffSelected(ids));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred rejecting timeoff records");
            }
        }


        [HttpPut("timeoff/reject")]
        public IActionResult RejectTimesheetsRequests([FromBody] int[] ids)
        {
            try
            {
                return Ok(_approvalBusiness.RejectTimesheetsRequests(ids));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred rejecting timesheets records");
            }
        }


        [HttpPost("timeoff/delete")]
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
}
