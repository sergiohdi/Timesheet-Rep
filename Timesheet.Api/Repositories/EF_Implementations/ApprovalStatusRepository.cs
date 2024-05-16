using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Repositories.EF_Implementations;

public class ApprovalStatusRepository : IApprovalStatusRepository
{
    private readonly TimesheetContext _db;
    private readonly IMapper _mapper;

    public ApprovalStatusRepository(TimesheetContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public IEnumerable<ApprovalStatusDto> GetApprovalStatuses()
    {
        var approvalStatuses = _db.ApprovalStatus.AsNoTracking().ToList();
        return _mapper.Map<IEnumerable<ApprovalStatusDto>>(approvalStatuses);
    }
}
