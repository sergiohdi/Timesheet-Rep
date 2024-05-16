using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Repositories.EF_Implementations;

public class ApprovalHistoryRepository : IApprovalHistoryRepository
{
    private readonly TimesheetContext _db;
    private readonly IMapper _mapper;

    public ApprovalHistoryRepository(TimesheetContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public IEnumerable<GetApprovalHistoryDto> GetApprovalsHistory(int timesheetId)
    {
        List<ApprovalHistory> approvalsHistory = _db.ApprovalHistory.Where(ah => ah.IdTimesheetControl == timesheetId).ToList();
        return _mapper.Map<IEnumerable<ApprovalHistory>, IEnumerable<GetApprovalHistoryDto>>(approvalsHistory);
    }

    public bool CreateApprovalHistory(CreateApprovalRequestDto request)
    {
        ApprovalHistory approvalHistory = _mapper.Map<CreateApprovalRequestDto, ApprovalHistory>(request);
        _db.ApprovalHistory.Add(approvalHistory);
        return _db.SaveChanges() > 0;
    }

    public bool DeleteApprovalHistory(int approvalId)
    {
        _db.ApprovalHistory.Entry(new ApprovalHistory { Id = approvalId }).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        return _db.SaveChanges() > 0;
    }

    public bool DeleteRegularApprovalHistoryRecords(int[] userIds, int[] timesheetControlIds)
    {
        _db.ApprovalHistory.Where(x =>
           x.TimesheetType == (int)ApprovalType.RegularTime &&
           userIds.Contains(x.IdUser.Value) &&
           timesheetControlIds.Contains(x.IdTimesheetControl.Value)
        )
        .ExecuteDelete();

        return true;
    }
}
