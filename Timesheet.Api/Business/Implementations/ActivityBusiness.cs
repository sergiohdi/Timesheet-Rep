using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations;

public class ActivityBusiness : IActivityBusiness
{
    private readonly IActivityRepository _activityRepository;
    private readonly ITimesheetDataRepository _timesheetDataRepository;

    public ActivityBusiness(
        IActivityRepository activityRepository, 
        ITimesheetDataRepository timesheetDataRepository,
        IUserActivityCodeRepository userActivityCodeRepository)
    {
        _activityRepository = activityRepository;
        _timesheetDataRepository = timesheetDataRepository;
    }

    public IEnumerable<ActivityDto> GetActivities(bool? disabled) => _activityRepository.GetActivities(disabled);

    public IEnumerable<ActivityDto> GetActivitiesForUser() => _activityRepository.GetActivitiesForUser();

    public ActivityDto GetActivityById(int activityId) => _activityRepository.GetActivityById(activityId);

    public bool CreateActivity(ActivityDto activity) => _activityRepository.CreateActivity(activity);

    public bool UpdateActivity(ActivityDto activity) => _activityRepository.UpdateActivity(activity);

    public bool DeleteActivity(int activityId)
    {
        // Validate if the activity exists
        ActivityDto activity = _activityRepository.GetActivityById(activityId);
        if (activity is null)
        {
            return false;
        }
        
        // Validate if the activity is used in any Timesheet
        bool isUsedActivityInTimesheet = _timesheetDataRepository.ValidateTimesheetByActivityId(activityId);
        
        // validate if the activity is used in any UserActivityCode
        if (isUsedActivityInTimesheet)
        {
            return false;
        }

        bool result;
        try
        {
            result = _activityRepository.DeleteActivity(activity);
        }

        catch (System.Exception ex)
        {
            if (!ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE"))
            {
                throw;
            }

            result = false;
        }

        return result;            
    }
}
