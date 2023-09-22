using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces
{
    public interface IActivityRepository
    {
        IEnumerable<ActivityDto> GetActivities(bool? disabled);

        IEnumerable<ActivityDto> GetActivitiesForUser();

        ActivityDto GetActivityById(int activityId);

        bool CreateActivity(ActivityDto activity);

        bool UpdateActivity(ActivityDto activity);

        bool DeleteActivity(ActivityDto activity);

        //bool UpdateActivityState(ActivityDto activity);

        //Check this one with the team
        //bool ValidateActivitiesByClientId(int activityId);
    }
}