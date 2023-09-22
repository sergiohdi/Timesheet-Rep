using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using Timesheet.Api.Business.Interfaces;

namespace Timesheet.Api.Controllers
{
    [ApiController]
    [Route("api/employeetype")]
    public class EmployeeTypeController: ControllerBase
    {
        private readonly IEmployeeTypeBusiness _employeeTypeBusiness;
        private readonly ILogger<EmployeeTypeController> _logger;

        public EmployeeTypeController(IEmployeeTypeBusiness employeeTypeBusiness, ILogger<EmployeeTypeController> logger)
        {
            _employeeTypeBusiness = employeeTypeBusiness;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetEmployeeTypes()
        {
            try
            {
                return Ok(_employeeTypeBusiness.GetEmployeeTypes().ToList());
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred getting employee types");
            }
        }
    }
}
