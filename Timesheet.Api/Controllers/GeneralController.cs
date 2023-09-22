using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheet.Api.Business.Interfaces;

namespace Timesheet.Api.Controllers
{
    [ApiController]
    [Route("api/general")]
    public class GeneralController : ControllerBase
    {
        private readonly IGeneralBusiness _generalBusiness;
        private readonly ILogger<ActivityController> _logger;

        public GeneralController(IGeneralBusiness generalBusiness, ILogger<ActivityController> logger)
        {
            _generalBusiness = generalBusiness;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetGeneralRecords(string group)
        {
            try
            {
                return Ok(_generalBusiness.GetGeneralRecords(group));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occured getting general records");
            }
        }

        [HttpGet("weekenddates")]
        public IActionResult GetWeekendDates()
        {
            try
            {
                return Ok(_generalBusiness.GetWeekendDates());
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occured getting weekend dates");
            }
        }
    }
}
