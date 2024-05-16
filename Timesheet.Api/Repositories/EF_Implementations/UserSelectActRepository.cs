using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Repositories.EF_Implementations;

public class UserSelectActRepository : IUserSelectActRepository
{
    private readonly TimesheetContext _db;
    private readonly IMapper _mapper;

    public UserSelectActRepository(TimesheetContext db, IMapper mapper ) 
    {
        _db = db;
        _mapper = mapper;
    }

    public UserSelectActDto GetUserPreferences(int userId)
    {
        UserSelectAct userPreference = _db.UserSelectAct.AsNoTracking().FirstOrDefault(x => x.UserId == userId);
        return _mapper.Map<UserSelectActDto>(userPreference);
    }

    public void UpdateUserPreferences(UserSelectActDto userPreferences)
    {
        var existingUserPrefereces = _db.UserSelectAct
            .FirstOrDefault(x => x.UserId == userPreferences.UserId);

        if (existingUserPrefereces is null)
        {
            _db.Add(_mapper.Map<UserSelectAct>(userPreferences));
        }
        else
        {
            existingUserPrefereces.Activities = userPreferences.Activities;
        }

        _db.SaveChanges();
    }
}
