using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Text.Json;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Business.Implementations;

namespace Timesheet.Api.Controllers
{
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

        [HttpGet("{period}")]
        public IActionResult GetTimesheetControlRecord([FromRoute] string period)
        {
            try
            {
                int userId = 579;
                DateTime.TryParse(period, out DateTime datePeriod);
                return Ok(_timesheetControlBusiness.GetTimesheetControlRecord(datePeriod, userId));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred getting Timesheet Control record");
            }
        }

        [HttpPut]
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
                // Todo this value will be replaced by principal info after get JWT token
                int userId = 579;
                Dictionary<string, string> jsonObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(element.ToString());
                DateTime.TryParse(jsonObject["period"], out DateTime period);

                return Ok(_timesheetControlBusiness.CreateTimesheetControlRecord(period, userId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred creating a Timesheet Control");
            }
        }

        [HttpPost("TimesheetControl")]
        public IActionResult GetTimesheetControlRequests([FromBody] JsonElement element)
        {
            try
            {
                int userId = 0;
                var paramsObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(element.ToString());
                DateTime.TryParse(paramsObj["startDate"], out DateTime startDate);
                DateTime.TryParse(paramsObj["endDate"], out DateTime endDate);
                bool.TryParse(paramsObj["isSupervisor"], out bool isSupervisor);
                //if (isSupervisor)
                //{
                //    userId = 561; // Get from token
                //}
                return Ok(_timesheetControlBusiness.GetTimesheetControlRequests(startDate, endDate, userId));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred fetching timesheetcontrol records");
            }

        }

        [HttpPut("timesheetcontrol/approve")]
        public IActionResult ApproveTimesheetsRequests([FromBody] int[] ids)
        {
            try
            {
                return Ok(_timesheetControlBusiness.ApproveTimesheetsRequests(ids));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred approving timesheets records");
            }
        }

        [HttpPut("timesheetcontrol/reopen")]
        public IActionResult ReopenTimesheetsRequests([FromBody] int[] ids)
        {
            try
            {
                return Ok(_timesheetControlBusiness.ReopenTimesheetsRequests(ids));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred reopening timesheets records");
            }
        }

        [HttpPost("timesheetcontrol/delete")]
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
}
