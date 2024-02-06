using System;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Business.Implementations
{
    public class TimesheetControlBusiness : ITimesheetControlBusiness
    {
        private readonly ITimesheetControlRepository _timesheetControlRepository;
        private readonly IApprovalRepository _approvalRepository;
        private readonly ITimesheetDataRepository _timesheetDataRepository;
        private readonly IUserRepository _userRepository;

        public TimesheetControlBusiness(
            ITimesheetControlRepository timesheetControlRepository,
            IApprovalRepository approvalRepository,
            ITimesheetDataRepository timesheetDataRepository,
            IUserRepository userRepository
        )
        {
            _timesheetControlRepository = timesheetControlRepository;
            _approvalRepository = approvalRepository;
            _timesheetDataRepository = timesheetDataRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<TimesheetControlDto> GetTimesheetControl()
        {
            return _timesheetControlRepository.GetTimesheetControl();
        }

        public TimesheetControlDto GetTimesheetControlRecord(DateTime period, int userId)
        {
            return _timesheetControlRepository.GetTimesheetControlRecord(period, userId);
        }

        public bool UpdateApprovalStatus(int[] ids)
        {
            //IEnumerable<TimesheetControlDto> records = _timesheetControlRepository.GetTimesheetControlById(ids);

            //// Update timesheetcontrol table
            //_timesheetControlRepository.UpdateApprovalStatus(ids);

            //List<int> userIds = records.Select(x => x.UserId).Distinct().ToList();
            //List<DateTime> periods = records.Select(x => x.TimesheetPeriod).Distinct().ToList();

            //// Update approvals table
            //_approvalRepository.UpdateApprovalStatus(periods, userIds);

            //// Update timesheet table
            //_timesheetDataRepository.UpdateApprovedRecords(periods, userIds);

            return true;
        }

        public TimesheetControlDto CreateTimesheetControlRecord(DateTime period, int userId)
        {
            UserDto existingUser = _userRepository.GetUser(userId);
            if (existingUser == null) 
            {
                return null;
            }

            TimesheetControlDto response = _timesheetControlRepository.GetTimesheetControlByPeriodAndUserId(period, userId);
            if (response != null)
            {
                return response;
            }

            return _timesheetControlRepository.CreateTimesheetControlRecord(new TimesheetControlDto
            {
                TimesheetPeriod = period,
                UserId = userId,
                StartDate = period,
                EndDate = DateFunctions.GetPeriodLastDate(period),
                ApprovalStatusId = (int)ApprovalStatusOption.NotSubmitted,
                UserTemplateId = existingUser.TimesheetTemplate ?? Utils.Constants.EIGHHOURSSHIFT // This line is temporary
            });
        }

        public IEnumerable<TimesheetControlApprovalDto> GetTimesheetControlRequests(DateTime startDate, DateTime endDate, int userId = 0)
        {
            return _timesheetControlRepository.GetTimesheetControlRequests(startDate, endDate, userId);
        }

        public bool ApproveTimesheetsRequests(int[] ids, bool isWTF = true)
        {
            // get timesheets control requests
            var timesheetsRequests = _timesheetControlRepository.GetTimesheetsApprovalRecords(ids);

            // update timesheets control requests
            bool response = _timesheetControlRepository.ProcessTimesheetsRequests(ids, isWTF
                ? (int)ApprovalStatusOption.Approved
                : (int)ApprovalStatusOption.SupervisorApproval);


            // Todo update approvals if there are any
            foreach (var item in timesheetsRequests)
            {
                var approvals = _approvalRepository.GetRegularTimeApprovals(item.UserId, item.TimesheetPeriod);
                if (approvals.Any())
                {
                    _approvalRepository.ProcessRequests(approvals.Select(x => x.ApprovalId).ToArray(), isWTF
                                                                      ? (int)ApprovalStatusOption.Approved
                                                                      : (int)ApprovalStatusOption.SupervisorApproval);
                }
            }
                
            if (isWTF)
            {
                // update timesheet records
                foreach (var item in timesheetsRequests)
                {
                            _timesheetDataRepository.UpdateRegularRecords(
                            item.UserId,
                            item.TimesheetPeriod.Date,
                            (int)ApprovalStatusOption.Approved);                   
                }
            }

            return true;
        }

        public bool ReopenTimesheetsRequests(int[] ids, bool isWTF = true)
        {
            // get timesheets control requests
            var timesheetsRequests = _timesheetControlRepository.GetTimesheetsApprovalRecords(ids);

            // update timesheets control requests
            bool response = _timesheetControlRepository.ProcessTimesheetsRequests(ids,(int)ApprovalStatusOption.NotSubmitted);

            // Todo update approvals if there are any
            foreach (var item in timesheetsRequests)
            {
                var approvals = _approvalRepository.GetRegularTimeApprovals(item.UserId, item.TimesheetPeriod);
                if (approvals.Any())
                {
                    _approvalRepository.ProcessRequests(approvals.Select(x => x.ApprovalId).ToArray(), (int)ApprovalStatusOption.NotSubmitted);                                                                      
                }
            }

            if (isWTF)
            {
                // update timesheet records
                foreach (var item in timesheetsRequests)
                {
                    _timesheetDataRepository.UpdateRegularRecords(
                    item.UserId,
                    item.TimesheetPeriod.Date,
                    (int)ApprovalStatusOption.NotSubmitted);
                }
            }

            return true;
        }

        public bool DeleteTimesheetsRequests(int[] ids)
        {
            //get timesheets control requests
            var timesheetsRequests = _timesheetControlRepository.GetTimesheetsApprovalRecords(ids);

            //delete timesheets control records
            _timesheetControlRepository.DeleteTimesheetsApprovalRequests(ids);

            // delete timesheet rows
            foreach (var item in timesheetsRequests)
            {
                _timesheetDataRepository.DeleteTimesheetOnlyRows(item.TimesheetPeriod, item.UserId); 
            }

            // Delete approval records if there are any for the selected timesheets
            
            foreach (var item in timesheetsRequests)
            {               
                _approvalRepository.DeleteTimesheetRecordApproval(item.TimesheetPeriod, item.UserId);          
            }

            return true;
        }
    }
}
