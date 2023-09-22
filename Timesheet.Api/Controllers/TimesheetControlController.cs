using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;

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

        [HttpPut()]
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
    }
}
