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

public class CostCenterRepository : ICostCenterRepository
{
    private readonly TimesheetContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public CostCenterRepository(TimesheetContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public IEnumerable<CostCenterDto> GetCostCenters(bool? disabled)
    {
        IQueryable<CostCenter> result = _db.CostCenter.AsNoTracking();

        if (disabled.HasValue && disabled.Value == false)
        {
            result = result.Where(x => (bool)!x.Disabled).OrderBy(x=>x.Costcentername);
        }

        return _mapper.Map<IEnumerable<CostCenterDto>>(result.ToList());
    }

    public CostCenterDto GetCostCenterById(int costCenterId) => 
        _mapper.Map<CostCenterDto>(_db.CostCenter.AsNoTracking().FirstOrDefault(x => x.Costcenterid == costCenterId));

    public bool CreateCostCenter(CostCenterDto costCenter)
    {
        CostCenter costCenterDb = _mapper.Map<CostCenter>(costCenter);
        costCenterDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

        _db.CostCenter.Add(costCenterDb);
        return _db.SaveChanges() > 0;
    }

    public bool UpdateCostCenter(CostCenterDto costCenter)
    {
        CostCenter costCenterDb = _mapper.Map<CostCenter>(costCenter);
        costCenterDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

        _db.Entry(costCenterDb).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        return _db.SaveChanges() > 0;
    }

    public bool UpdateCostCenterState(CostCenterDto costCenter)
    {
        CostCenter costCenterDb = _mapper.Map<CostCenter>(costCenter);
        costCenterDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

        _db.Entry(costCenterDb).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        return _db.SaveChanges() > 0;
    }
}
