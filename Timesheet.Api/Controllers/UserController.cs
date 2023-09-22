using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly ISubstituteBusiness _substituteBusiness;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IUserBusiness userBusiness, 
            ISubstituteBusiness substituteBusiness,
            ILogger<UserController> logger)
        {
            _userBusiness = userBusiness;
            _substituteBusiness = substituteBusiness;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetUsers(bool? disabled)
        {
            try
            {
                return Ok(_userBusiness.GetUsers(disabled));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred getting users");
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                return Ok(_userBusiness.GetUser(userId));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred getting user");
            }
        }

        [HttpPost]
        public IActionResult CreateUser(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(_userBusiness.CreateUser(user));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred creating user");
            }
        }

        [HttpPut]
        public IActionResult UpdateUser(UserDto user)
        {
            try
            {
                return Ok(_userBusiness.UpdateUser(user));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred updating user");
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                return Ok(_userBusiness.DeleteUser(userId));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred deleting user");
            }
        }

        [HttpGet("{userId}/substitutes")]
        public IActionResult GetSubstitutes(int userId)
        {
            try
            {
                return Ok(_substituteBusiness.GetSubstitutesByUserId(userId));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred getting substitutes");
            }
        }

        [HttpPost("{userId}/substitutes")]
        public IActionResult CreateSubstitute([FromRoute] int userId, [FromBody]List<SubstituteDto> substitutes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(_substituteBusiness.UpdateSubstitutes(substitutes, userId));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred creating substitute");
            }
        }
    }
}
