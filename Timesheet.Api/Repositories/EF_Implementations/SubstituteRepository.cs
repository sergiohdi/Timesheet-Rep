 using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Repositories.EF_Implementations;

public class SubstituteRepository: ISubstituteRepository
{
    private readonly TimesheetContext _db;
    private readonly IMapper _mapper;

    public SubstituteRepository(TimesheetContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public IEnumerable<SubstituteDto> GetUsersSubstituteByUserId(int userId)
    {
        IEnumerable<Substitute> userSubstitutes = _db.Substitute.Where(x => x.UserId == userId ).ToList();                                                                                                                                                           
        return _mapper.Map<IEnumerable<SubstituteDto>>(userSubstitutes);
    }

        public IEnumerable<KeyValuePair<int, string>> GetSubstitutesByEmp(int userId)
        {
            var userSubstitutes = _db.Substitute
                .Where(s => s.UserId == userId)
                .Join(_db.User,
                      substitute => substitute.SubstituteId,
                      user => user.Id,
                      (substitute, user) => new { Substitute = substitute, User = user })
                .Where(x => x.User.Disabled == false)
                .Select(x => new KeyValuePair<int, string>(x.Substitute.SubstituteId, x.User.LoginName))
                .ToList();

            return userSubstitutes;
        }

        public void RemoveSubstitutes(int userId)
        {
            _db.Substitute.Where(x => x.UserId == userId).ExecuteDelete();

        }

    public bool InsertSubstitutes(List<SubstituteDto> substitutes)
    {
        List<Substitute> userSubstitutes = _mapper.Map<List<Substitute>>(substitutes);
        _db.Substitute.AddRange(userSubstitutes);
        return _db.SaveChanges() > 0;
    }
}

