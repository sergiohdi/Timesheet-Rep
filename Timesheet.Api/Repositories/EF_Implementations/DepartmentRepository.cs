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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly TimesheetContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public DepartmentRepository(TimesheetContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public IEnumerable<DepartmentDto> GetDepartments(bool? disabled)
        {
            IQueryable<Department> result = _db.Department.AsNoTracking().OrderBy(x => x.Name);

            if (disabled != null)
            {
                result = result.Where(x => x.DisabledSetting == disabled);
            }

            return _mapper.Map<IEnumerable<DepartmentDto>>(result.ToList());
        }

        public DepartmentDto GetDepartmentById(int id)
        {
            Department department = _db.Department.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return _mapper.Map<DepartmentDto>(department);
        }

        public bool CreateDepartment(DepartmentDto department)
        {
            Department departmentDb = _mapper.Map<Department>(department);
            departmentDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

            _db.Department.Add(departmentDb);
            return _db.SaveChanges() > 0;
        }

        public bool UpdateDepartment(DepartmentDto department)
        {
            Department departmentDb = _mapper.Map<Department>(department);
            departmentDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

            _db.Entry(departmentDb).State = EntityState.Modified;
            return _db.SaveChanges() > 0;
        }


        // Soft Delete
        //public bool UpdateDepartmentState(DepartmentDto department)
        //{
        //    Department departmentDb = _mapper.Map<Department>(department);
        //    departmentDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

        //    _db.Entry(departmentDb).State = EntityState.Modified;
        //    return _db.SaveChanges() > 0;
        //}
        public bool DeleteDepartment(DepartmentDto department)
        {
            Department DepartmentDb = _mapper.Map<Department>(department);
            _db.Remove(DepartmentDb);
            return _db.SaveChanges() > 0;
        }
    }
}
