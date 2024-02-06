using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Api.Repositories.Repositories.EF_Implementations;

namespace Timesheet.Api.Business.Implementations
{
    public class ActivityBusiness : IActivityBusiness
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ITimesheetDataRepository _timesheetDataRepository;
        private readonly IUserActivityCodeRepository _userActivityCodeRepository;

        public ActivityBusiness(
            IActivityRepository activityRepository, 
            ITimesheetDataRepository timesheetDataRepository,
            IUserActivityCodeRepository userActivityCodeRepository)
        {
            _activityRepository = activityRepository;
            _timesheetDataRepository = timesheetDataRepository;
            _userActivityCodeRepository = userActivityCodeRepository;
        }
        public IEnumerable<ActivityDto> GetActivities(bool? disabled)
        {
            return _activityRepository.GetActivities(disabled);
        }

        public IEnumerable<ActivityDto> GetActivitiesForUser()
        {
            return _activityRepository.GetActivitiesForUser();
        }

        public ActivityDto GetActivityById(int activityId)
        {
            return _activityRepository.GetActivityById(activityId);
        }

        public bool CreateActivity(ActivityDto activity)
        {
            return _activityRepository.CreateActivity(activity);
        }

        public bool UpdateActivity(ActivityDto activity)
        {
            return _activityRepository.UpdateActivity(activity);
        }

        //public bool UpdateActivityState(int activityId)
        //{
        //    ActivityDto activity = _activityRepository.GetActivityById(activityId);
        //    activity.Disabled = !activity.Disabled;

        //    return _activityRepository.UpdateActivityState(activity);
        //}

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
}
