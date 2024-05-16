using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Extensions;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Repositories.EF_Implementations;

public class UserActivityCodeRepository : IUserActivityCodeRepository
{
    private readonly TimesheetContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public UserActivityCodeRepository(TimesheetContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public IEnumerable<UserActivityCodeDto> GetUsersActivitiesByUserId(int userId)
    {
        IEnumerable<UserActivityCode> userActivities = _db.UserActivityCode.Where(x => x.UserId == userId && x.IsActivityEnabled.Value == true)
                                                                           .OrderBy(x => x.ActivityName)
                                                                           .ToList();
        return _mapper.Map<IEnumerable<UserActivityCodeDto>>(userActivities);
    }

    public void RemoveUserActivities(int userId)
    {
         _db.UserActivityCode.Where(x => x.UserId == userId).ExecuteDelete();
        
    }

    public IEnumerable<UserActivityCodeLightDto> GetUsersActivitiesByUserIdForDropDown(int userId)
    {
        return _db.UserActivityCode.AsNoTracking()
                                    .Join(
                                        _db.Activity,
                                        uac => uac.ActivityId,
                                        a => a.ActivityId,
                                        (uac, a) => new { uac, a })
                                   .Where(x => x.uac.UserId == userId && x.uac.IsActivityEnabled.Value)
                                   .OrderBy(x => x.a.ActivityName)
                                   .Select(x => new UserActivityCodeLightDto
                                   {
                                       ActivityId = x.uac.ActivityId,
                                       ActivityName = x.a.ActivityName,
                                       ActivityCode = x.a.ActivityCode
                                   })
                                   .ToList();
    }

    public UserActivityCodeDto GetUserActivityCode(int userActivityCodeId)
    {
        UserActivityCode userActivityCode = _db.UserActivityCode.FirstOrDefault(x => x.UserActivityCodeId == userActivityCodeId);
        return _mapper.Map<UserActivityCodeDto>(userActivityCode);
    }

    public bool InsertUserActivities(List<UserActivityCodeDto> activities)
    {
        List<UserActivityCode> userActivities = _mapper.Map<List<UserActivityCode>>(activities);     
        userActivities.ForEach(x => x.SetAudit(_httpContextAccessor.HttpContext, true, 579));

        _db.UserActivityCode.AddRange(userActivities);
        return _db.SaveChanges() > 0;
    }
}
