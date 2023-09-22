using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Business.Implementations;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Controllers
{
    [ApiController]
    [Route("api/projecthasuser")]
    public class ProjectHasUserController : ControllerBase
    {
        private readonly IProjectHasUserBusiness _projecthasuserBusiness;
        private readonly ILogger<ProjectHasUserController> _logger;

        public ProjectHasUserController(IProjectHasUserBusiness projectHasUserBusiness, ILogger<ProjectHasUserController> logger)
        {
            _projecthasuserBusiness = projectHasUserBusiness;
            _logger = logger;
        }

        [HttpGet("{projectId}")]
        public IActionResult GetUsersByProject(int projectId)
        {
            if (!ModelState.IsValid || projectId == 0)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(_projecthasuserBusiness.GetUsersByProject(projectId));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred getting projects for user");
            }
        }

        [HttpPost("{projectId}")]
        public IActionResult AddUsersToTeamProject([FromRoute] int projectId, [FromBody] IEnumerable<int> usersId) 
        {
            if (!ModelState.IsValid || projectId == 0 || usersId.Count() == 0)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(_projecthasuserBusiness.AddUsersToTeamProject(projectId, usersId));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred creating team project members");
            }
        }
    }
}
