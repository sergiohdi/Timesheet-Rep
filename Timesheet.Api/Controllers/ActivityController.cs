using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Controllers
{
    [ApiController]
    [Route("api/activity")]
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
}
