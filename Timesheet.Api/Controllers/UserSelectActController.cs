using Microsoft.AspNetCore.Mvc;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSelectActController : ControllerBase
    {
        private readonly IUserSelectActBusiness _userSelectActBusiness;

        public UserSelectActController(IUserSelectActBusiness userSelectActBusiness)
        {
            _userSelectActBusiness = userSelectActBusiness;
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserPreferences(int userId)
        {
            UserSelectActDto userPreference = _userSelectActBusiness.GetUserPreferences(userId) ?? new UserSelectActDto();
            return Ok(userPreference);
        }
    }
}
