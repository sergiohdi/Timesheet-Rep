using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Repositories.EF_Implementations
{
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
    }
}
