using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Models.Requests;
using Timesheet.Api.Utils;

namespace Timesheet.Api.Controllers
{
    [ApiController]
    [Route("api/timesheetdata")]
    public class TimesheetDataController : ControllerBase
    {
        private readonly ITimesheetDataBusiness _timesheetDataBusiness;
        private readonly IUserSelectActBusiness _userSelectActBusiness;
        private readonly ILogger<TimesheetDataController> _logger;

        public TimesheetDataController(
            ITimesheetDataBusiness timesheetDataBusiness, 
            IUserSelectActBusiness userSelectActBusiness,
            ILogger<TimesheetDataController> logger)
        {
            _timesheetDataBusiness = timesheetDataBusiness;
            _userSelectActBusiness = userSelectActBusiness;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetTimesheetData([FromQuery] int userId, [FromQuery] int year, [FromQuery] int month, [FromQuery] int period)
        {
            if (userId == 0 || year == 0 || month == 0 || period == 0)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(_timesheetDataBusiness.GetTimesheetData(new TimesheetRequestDto 
                {
                    UserId = userId,
                    Year = year,
                    Month = month,
                    Period = period
                }));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred getting timesheet data");
            }
        }

        [HttpPut]
        public IActionResult UpdateTimesheetBaseProperties([FromBody] UpdateTimesheetBasePropertiesRequest request)
        {
            _logger.LogInformation("Enter to update timesheet base properties endpoint");

            if (request == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                List<TimesheetItemDto> items = new List<TimesheetItemDto>();
                UserSelectActDto existingUserPreferences = _userSelectActBusiness.GetUserPreferences(request.UserId);
                if (existingUserPreferences != null)
                {
                    items = JsonConvert.DeserializeObject<List<TimesheetItemDto>>(existingUserPreferences.Activities);
                }

                // Update user preferences
                if (request.IsBaseProperty)
                {
                    _userSelectActBusiness.UpdateUserPreferences(request, items);
                }

                // Update timesheet records
                if (request.Action == TimesheetItemAction.Update && request.TimesheetItem.Entries.Exists(x => x.Id > 0))
                {
                    _timesheetDataBusiness.UpdateTimesheetBaseInformation(request.TimesheetItem, request.Property);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred updating user preferences");
            }

            return Ok();
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateTimesheetHours([FromRoute] int userId, [FromBody] TimesheetItemDto record)
        {
            if (!ModelState.IsValid || record == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok( _timesheetDataBusiness.UpdateTimesheetHours(userId, record) );
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred updating user hours");
            }
        }

        [HttpPost("{userId}")]
        public IActionResult DeleteTimesheetRecord([FromRoute] int userId, [FromBody] TimesheetItemDto item)
        {
            if (!ModelState.IsValid || item is null)
            {
                return BadRequest(ModelState);
            }

            UserSelectActDto currentUserPreferences = _userSelectActBusiness.GetUserPreferences(userId);
            if (currentUserPreferences is null)
            {
                return NotFound();
            }

            // Delete user preferences
            _userSelectActBusiness.DeleteUserPreferences(
                userId, 
                item.Id, 
                JsonConvert.DeserializeObject<List<TimesheetItemDto>>(currentUserPreferences.Activities));

            // Delete timesheet records
            _timesheetDataBusiness.DeleteTimesheetRecords(item);

            return Ok();
        }
    }
}
