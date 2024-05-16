using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces;

public interface IUserSelectActRepository
{
    UserSelectActDto GetUserPreferences(int userId);
    void UpdateUserPreferences(UserSelectActDto userPreferences);
}
