using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Controllers
{
    [ApiController]
    [Route("api/approvalhistory")]
    public class ApprovalHistoryController : ControllerBase
    {
        private readonly IApprovalHistoryBusiness _approvalHistoryBusiness;
        private readonly ILogger<ApprovalHistoryController> _logger;

        public ApprovalHistoryController(
            IApprovalHistoryBusiness approvalHistoryBusiness,
            ILogger<ApprovalHistoryController> logger
        )
        {
            _approvalHistoryBusiness = approvalHistoryBusiness;
            _logger = logger;
        }

        [HttpGet("{timesheetId}")]
        public IActionResult GetApprovalsHistory(int timesheetId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(_approvalHistoryBusiness.GetApprovalsHistory(timesheetId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred deleting timeoff record");
            }
        }

        [HttpPost]
        public IActionResult AddApprovalHistory(CreateApprovalRequestDto approvalHistory)
        {
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Temp lines
                approvalHistory.IdUser = 579;
                approvalHistory.UserName = "Sergio Barbosa";
                // Temp lines
                return Ok(_approvalHistoryBusiness.CreateApprovalHistory(approvalHistory));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error has occurred deleting timeoff record");
            }
        
        }
    }
}
