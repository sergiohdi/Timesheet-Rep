using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheet.Api.Business.Interfaces;

namespace Timesheet.Api.Controllers
{
    [ApiController]
    [Route("api/timeoff")]
    public class TimeOffController : ControllerBase
    {
        private readonly ITimeOffBusiness _timeOffBusiness;
        private readonly ILogger<TimeOffController> _logger;

        public TimeOffController(ITimeOffBusiness timeOffBusiness,  ILogger<TimeOffController> logger)
        {
            _timeOffBusiness = timeOffBusiness;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetTimeOffList()
        {
            try
            {
                return Ok(_timeOffBusiness.GetTimeOffList());
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred getting time off list");
            }
        }
    }
}
