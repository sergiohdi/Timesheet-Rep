using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Models.Requests;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Api.Utils;

namespace Timesheet.Api.Business.Implementations;

public class UserSelectActBusiness : IUserSelectActBusiness
{
    private readonly IUserSelectActRepository _userSelectActRepository;

    public UserSelectActBusiness(IUserSelectActRepository userSelectActRepository) => _userSelectActRepository = userSelectActRepository;

    public UserSelectActDto GetUserPreferences(int userId) => _userSelectActRepository.GetUserPreferences(userId);

    public void UpdateUserPreferences(int userId, UpdateTimesheetBasePropertiesRequest request, List<TimesheetItemDto> items) 
    {
        IEnumerable<TimesheetItemDto> updatedPreferences = ProcessUserPreferences(request, items);
        _userSelectActRepository.UpdateUserPreferences(new UserSelectActDto
        {
            UserId = userId,
            Activities = JsonConvert.SerializeObject(updatedPreferences
            .OrderBy(x => x.Id)
            .Select(x => new 
            {
                x.Id,
                x.ClientId,
                x.ProjectId,
                x.ActivityId
            }))
        });
    }

    public void DeleteUserPreferences(int userId, int itemId, List<TimesheetItemDto> items) 
    {
        _userSelectActRepository.UpdateUserPreferences(new UserSelectActDto
        {
            UserId = userId,
            Activities = JsonConvert.SerializeObject(items
            .Where(x => x.Id != itemId)
            .Select((x, idx) => new
            {
                x.Id,
                x.ClientId,
                x.ProjectId,
                x.ActivityId
            }))
        });
    }

    private static IEnumerable<TimesheetItemDto> ProcessUserPreferences(UpdateTimesheetBasePropertiesRequest request, List<TimesheetItemDto> items)
    {
        switch (request.Action)
        {
            case TimesheetItemAction.Add:
                items.Add(request.TimesheetItem);
                break;
            case TimesheetItemAction.Update:
                items = items.Where(x => x.Id != request.TimesheetItem.Id)
                             .ToList();
                items.Add(request.TimesheetItem);
                break;
            case TimesheetItemAction.Delete:
                items = items.Where(x => x.Id != request.TimesheetItem.Id)
                             .ToList();
                break;
            default:
                throw new System.Exception("This is not a known user prefernce action");
        }

        return items;
    }
}
