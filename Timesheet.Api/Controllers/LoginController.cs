using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginBusiness _loginBusiness;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILoginBusiness loginBusiness, ILogger<LoginController> logger)
        {
            _loginBusiness = loginBusiness;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("verifylogin")]
        public IActionResult VerifyLogin(LoginDto login)
        {
            try
            {
                return Ok(_loginBusiness.VerifyLogin(login));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Login error...");
            }
        }

        [HttpPost("verifychangepassword")]
        public IActionResult VerifyChangePassword(ChangePasswordDto login)
        {
            try
            {
                return Ok(_loginBusiness.VerifyChangePassword(login));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Password error...");
            }
        }

        [HttpPut("password")] 
        public IActionResult UpdatePassword([FromBody] ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {             
                return BadRequest("Invalid request data");
            }

            try
            {              
                return Ok(_loginBusiness.UpdatePassword(model));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred updating the password");
            }
        }

    }
}

