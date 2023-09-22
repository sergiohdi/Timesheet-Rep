using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheet.Api.Business.Implementations;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Controllers
{
    [ApiController]
    [Route("api/department")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentBusiness _departmentBusiness;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentBusiness departmentBusiness, ILogger<DepartmentController> logger)
        {
            _departmentBusiness = departmentBusiness;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetDepartments(bool? DisabledSetting)
        {
            try
            {
                return Ok(_departmentBusiness.GetDepartments(DisabledSetting));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred getting departments");
            }
        }

        [HttpGet("{Id}")]
        public IActionResult GetDepartmentById(int Id)
        {
            try
            {
                return Ok(_departmentBusiness.GetDepartmentById(Id));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred getting department");
            }
        }

        [HttpPost]
        public IActionResult CreateDepartment(DepartmentDto department)
        {
            try
            {
                return Ok(_departmentBusiness.CreateDepartment(department));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred creating department");
            }
        }

        [HttpPut]
        public IActionResult UpdateDepartment(DepartmentDto department)
        {
            try
            {
                return Ok(_departmentBusiness.UpdateDepartment(department));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred updating department");
            }
        }

        //[HttpDelete("{departmentId}")]
        //public IActionResult ChangeActivityStatus(int departmentId)
        //{
        //    try
        //    {
        //        return Ok(_departmentBusiness.UpdateDepartmentState(departmentId));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return StatusCode(500, "An error occurred deleting department");
        //    }
        //}

        [HttpDelete("{Id}")]
        public IActionResult DeleteDepartment(int Id)
        {
            try
            {
                return Ok(_departmentBusiness.DeleteDepartment(Id));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred deleting department");
            }
        }
    }
}
