using System;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations
{
    public class TimesheetControlBusiness : ITimesheetControlBusiness
    {
        private readonly ITimesheetControlRepository _timesheetControlRepository;
        private readonly IApprovalRepository _approvalRepository;
        private readonly ITimesheetDataRepository _timesheetDataRepository;

        public TimesheetControlBusiness(
            ITimesheetControlRepository timesheetControlRepository,
            IApprovalRepository approvalRepository,
            ITimesheetDataRepository timesheetDataRepository
        )
        {
            _timesheetControlRepository = timesheetControlRepository;
            _approvalRepository = approvalRepository;
            _timesheetDataRepository = timesheetDataRepository;
        }
        public IEnumerable<TimesheetControlDto> GetTimesheetControl()
        {
            return _timesheetControlRepository.GetTimesheetControl();
        }
        public bool UpdateApprovalStatus(int[] ids)
        {
            IEnumerable<TimesheetControlDto> records = _timesheetControlRepository.GetTimesheetControlById(ids);

            // Update timesheetcontrol table
            _timesheetControlRepository.UpdateApprovalStatus(ids);

            List<int> userIds = records.Select(x => x.UserId).Distinct().ToList();
            List<DateTime> periods = records.Select(x => x.TimesheetPeriod).Distinct().ToList();

            // Update approvals table
            _approvalRepository.UpdateApprovalStatus(periods, userIds);

            // Update timesheet table
            _timesheetDataRepository.UpdateApprovedRecords(periods, userIds);

            return true;
        }
    }
}
