using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Models.Requests;

namespace Timesheet.Api.Business.Interfaces;

public interface IUserSelectActBusiness
{
    UserSelectActDto GetUserPreferences(int userId);
    void UpdateUserPreferences(int userId, UpdateTimesheetBasePropertiesRequest request, List<TimesheetItemDto> items);
    void DeleteUserPreferences(int userId, int itemId, List<TimesheetItemDto> items);
}
