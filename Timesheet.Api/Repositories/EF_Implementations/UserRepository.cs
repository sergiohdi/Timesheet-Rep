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
    public class UserRepository : IUserRepository
    {
        private readonly TimesheetContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UserRepository(TimesheetContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public IEnumerable<UserDto> GetUsers(bool? disabled)
        {
            
            IQueryable<User> result = _db.User.AsNoTracking().OrderBy(x => x.FirstName).ThenBy(x => x.LastName);


            if (disabled != null)
            {
                result = result.Where(x => x.Disabled == disabled);
            }

            return _mapper.Map<IEnumerable<UserDto>>(result.ToList());
        }

        public UserDto GetUser(int Id)
        {
            User user = _db.User.AsNoTracking().FirstOrDefault(x => x.Id == Id);
            return _mapper.Map<UserDto>(user);
        }

        public bool CreateUser(UserDto user)
        {
            User userDb = _mapper.Map<User>(user);
            userDb.Disabled = false;
            userDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

            _db.User.Add(userDb);
            return _db.SaveChanges() > 0;
        }

        public bool UpdateUser(UserDto user)
        {
            User userDb = _mapper.Map<User>(user);
            userDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

            _db.Entry(userDb).State = EntityState.Modified;
            return _db.SaveChanges() > 0;
        }

        public bool DeleteUser(UserDto user)
        {
            User userDb = _mapper.Map<User>(user);
            _db.Remove(userDb);

            return _db.SaveChanges() > 0;
        }
        public bool ValidateUsersByDepartmentId(int departmentId)
        {
            return _db.User.Any(x => x.DepartmentId == departmentId);
        }
    }
}
