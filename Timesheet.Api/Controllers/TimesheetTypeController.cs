using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.CustomFilters;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Controllers;

[ApiController]
[Route("api/timesheettype")]
[AuthorizeRoles((int)UserRole.Admin)]
public class TimesheetTypeController : ControllerBase
{
    private readonly ITimesheetTypeBusiness _timesheetTypeBusiness;
    private readonly ILogger<TimesheetTypeController> _logger;

    public TimesheetTypeController(ITimesheetTypeBusiness timesheetTypeBusiness, ILogger<TimesheetTypeController> logger)
    {
        _timesheetTypeBusiness = timesheetTypeBusiness;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetTimesheetTypes()
    {
        try
        {
            return Ok(_timesheetTypeBusiness.GetTimesheetTypes().ToList());
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred getting Timesheet-Types");
        }
    }
}
