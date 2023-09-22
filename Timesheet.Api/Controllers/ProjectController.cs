using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Controllers
{
    [ApiController]
    [Route("api/project")]
    public class ProjectController: ControllerBase
    {
        private readonly IProjectBusiness _projectBusiness;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(IProjectBusiness projectBusiness, ILogger<ProjectController> logger)
        {
            _projectBusiness = projectBusiness;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GeProjects(bool? isOpen)
        {
            try
            {
                return Ok(_projectBusiness.GetProjects(isOpen));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred getting projects");
            }
        }

        [HttpGet("fordrop")]
        public IActionResult GeProjectsForDropDown()
        {
            try
            {
                return Ok(_projectBusiness.GetProjectsForDropDown());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred getting projects");
            }
        }

        [HttpGet("{projectId}")]
        public IActionResult GetProject([FromRoute] int projectId)
        {
            try
            {
                return Ok(_projectBusiness.GetProjectById(projectId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred getting project");
            }
        }

        [HttpPost]
        public IActionResult CreateProject(ProjectDto project)
        {
            try
            {
                return Ok(_projectBusiness.CreateProject(project));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred creating project");
            }
        }

        [HttpPut]
        public IActionResult UpdateClient(ProjectDto project)
        {
            try
            {
                return Ok(_projectBusiness.UpdateProject(project));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred updating project");
            }
        }

        [HttpDelete("{projectId}")]
        public IActionResult DeleteProject(int ProjectId)
        {
            try
            {
                return Ok(_projectBusiness.DeleteProject(ProjectId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred deleting a project");
            }
        }

    }
}
