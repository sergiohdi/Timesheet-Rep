using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.CustomFilters;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Controllers;

[ApiController]
[Route("api/approvalstatus")]
[AuthorizeRoles((int)UserRole.Admin)]
public class ApprovalStatusController: ControllerBase
{
    private readonly IApprovalStatusBusiness _approvalStatusBusiness;
    private readonly ILogger<ApprovalStatusController> _logger;

    public ApprovalStatusController(IApprovalStatusBusiness approvalStatusBusiness, ILogger<ApprovalStatusController> logger)
    {
        _approvalStatusBusiness = approvalStatusBusiness;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetApprovalStatuses()
    {
        try
        {
            return Ok(_approvalStatusBusiness.GetApprovalStatuses());
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occured getting approval statuses");
        }
    }
}
