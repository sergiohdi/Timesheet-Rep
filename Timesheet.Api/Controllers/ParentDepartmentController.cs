using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.CustomFilters;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Controllers;

[ApiController]
[Route("api/parentdepartment")]
[AuthorizeRoles((int)UserRole.Admin)]
public class ParentDepartmentController : ControllerBase
{
    private readonly IParentDepartmentBusiness _parentdepartmentBusiness;
    private readonly ILogger<ParentDepartmentController> _logger;

    public ParentDepartmentController(IParentDepartmentBusiness parentdepartmentBusiness, ILogger<ParentDepartmentController> logger)
    {
        _parentdepartmentBusiness = parentdepartmentBusiness;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetParentDepartments()
    {
        try
        {
            return Ok(_parentdepartmentBusiness.GetParentDepartments());
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred getting parent departments");
        }
    }
}
