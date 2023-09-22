using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
namespace Timesheet.Api.Repositories.EF_Implementations
{
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
}

