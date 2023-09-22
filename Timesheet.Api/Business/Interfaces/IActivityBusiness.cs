using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces
{
    public interface IActivityBusiness
    {
        IEnumerable<ActivityDto> GetActivities(bool? disabled);

        IEnumerable<ActivityDto> GetActivitiesForUser();

        ActivityDto GetActivityById(int activityId);

        bool CreateActivity(ActivityDto activity);

        bool UpdateActivity(ActivityDto activity);

        //bool UpdateActivityState(int activityId);
        bool DeleteActivity(int activityId);
    }
}
