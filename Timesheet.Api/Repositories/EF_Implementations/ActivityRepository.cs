using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Extensions;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Repositories.EF_Implementations
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly TimesheetContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ActivityRepository(TimesheetContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public IEnumerable<ActivityDto> GetActivities(bool? disabled)
        {
            IQueryable<Activity> result = _db.Activity.AsNoTracking().OrderBy(x => x.ActivityName);

            if (disabled != null)
            {
                result = result.Where(x => x.Disabled == disabled);
            }

            return _mapper.Map<IEnumerable<ActivityDto>>(result.ToList());
        }

        public IEnumerable<ActivityDto> GetActivitiesForUser()
        {
            IQueryable<Activity> result = _db.Activity.AsNoTracking()
                                                      .Where(x => x.Disabled == false)
                                                      .OrderBy(x => x.ActivityName);

            return _mapper.Map<IEnumerable<ActivityDto>>(result.ToList());
        }

        public ActivityDto GetActivityById(int activityId)
        {
            Activity activity = _db.Activity.AsNoTracking().FirstOrDefault(x => x.ActivityId == activityId);
            return _mapper.Map<ActivityDto>(activity);
        }

        public bool CreateActivity(ActivityDto activity)
        {
            Activity activityDb = _mapper.Map<Activity>(activity);
            activityDb.Disabled = false;
            activityDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

            _db.Activity.Add(activityDb);
            return _db.SaveChanges() > 0;
        }

        public bool UpdateActivity(ActivityDto activity)
        {
            Activity activityDb = _mapper.Map<Activity>(activity);
            activityDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

            _db.Entry(activityDb).State = EntityState.Modified;
            return _db.SaveChanges() > 0;
        }

        // Soft Delete
        //public bool UpdateActivityState(ActivityDto activity)
        //{
        //    Activity activityDb = _mapper.Map<Activity>(activity);
        //    activityDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

        //    _db.Entry(activityDb).State = EntityState.Modified;
        //    return _db.SaveChanges() > 0;
        //}
       
        //public bool ValidateActivitiesByClientId(int clientId)
        //{
        //    return _db.Activity.Any(x => x.ClientId == clientId);
        //}

        public bool DeleteActivity(ActivityDto activity)
        {
            Activity ActivityDb = _mapper.Map<Activity>(activity);
            _db.Remove(ActivityDb);
            return _db.SaveChanges() > 0;
        }
    }
}
