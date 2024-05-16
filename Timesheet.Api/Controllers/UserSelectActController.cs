using Microsoft.AspNetCore.Mvc;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.CustomFilters;
using Timesheet.Api.Models.DTOs;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[AuthorizeRoles((int)UserRole.Admin)]
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
