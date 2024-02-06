using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Business.Implementations
{
    public class ApprovalBusiness : IApprovalBusiness
    {
        private readonly IApprovalRepository _approvalRepository;
        private readonly ITimeOffRepository _timeOffRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeTypeRepository _employeeTypeRepository;
        private readonly ITimesheetDataRepository _timesheetDataRepository;
        private readonly ITimesheetControlRepository _timesheetControlRepository;
        private readonly ILogger<ApprovalBusiness> _logger;

        public ApprovalBusiness(
            IApprovalRepository approvalRepository,
            ITimeOffRepository timeOffRepository,
            IDepartmentRepository departmentRepository,
            IUserRepository userRepository,
            IEmployeeTypeRepository employeeTypeRepository,
            ITimesheetDataRepository timesheetDataRepository,
            ITimesheetControlRepository timesheetControlRepository,
            ILogger<ApprovalBusiness> logger
        )
        {
            _approvalRepository = approvalRepository;
            _timeOffRepository = timeOffRepository;
            _departmentRepository = departmentRepository;
            _userRepository = userRepository;
            _employeeTypeRepository = employeeTypeRepository;
            _timesheetDataRepository = timesheetDataRepository;
            _timesheetControlRepository = timesheetControlRepository;
            _logger = logger;
        }

        public bool CreateTimeoffRequest(ApprovalDto approvalRequest)
        {
            DateTime periodStartDate = DateFunctions.GetPeriodStartDate(approvalRequest.StartDate);
            approvalRequest.Period = periodStartDate;

            bool isValidRange = ValidateDateRange(approvalRequest.Period.Value,
                                                approvalRequest.StartDate,
                                                approvalRequest.EndDate,
                                                approvalRequest.Duration.Value);
            if (!isValidRange) 
            {
                return false;
            }

            // Todo add logic to add data that should be come in the request
            bool response = _approvalRepository.SaveApprovalRequest(approvalRequest);
            bool timeOffResponse = CreateTimeoffRecords(approvalRequest);

            return response && timeOffResponse;
        }

        public bool CreateRegularTimeRequest(ApprovalDto approvalRequest, int userId)
        {
            // get all timesheet records acording to the requested period
            IEnumerable<TimesheetDataDto> regularTimeRecords = _timesheetDataRepository.GetRegularTimeData(
                approvalRequest.Period.Value,
                userId);

            // get current timesheetrecord 
            var timesheetControl = _timesheetControlRepository.GetTimesheetControlByPeriodAndUserId(
                approvalRequest.Period.Value,
                userId);

            // validate timesheet user template
            //bool isValidPeriod = ValidateRegularTimeRecords(timesheetControl.UserTemplateId, regularTimeRecords);
            //if (!isValidPeriod)
            //{
            //    _logger.LogError("User period doesn't have valid values on registered days");
            //    return false;
            //}

            // create new record on timesheet approval
            SetRegularTimeRequest(approvalRequest, userId);
            bool approvalRequestCreated = _approvalRepository.SaveApprovalRequest(approvalRequest);

            // update status in timesheet records
            bool timesheetRecordsUpdated = _timesheetDataRepository.UpdateRegularRecords(
                userId,
                approvalRequest.Period.Value,
                (int)ApprovalStatusOption.Waiting);

            // update status in timesheetcontrol record
            bool timesheetControlUpdated = _timesheetControlRepository.UpdateTimesheetContolRecords(
                userId,
                approvalRequest.Period.Value,
                (int)ApprovalStatusOption.Waiting);

            // send email to the supervisor

            return timesheetRecordsUpdated && timesheetControlUpdated && approvalRequestCreated;
        }

        public IEnumerable<ApprovalDto> GetTimeOffRecords(DateTime period)
        {
            return _approvalRepository.GetTimeOffRecords(period);
        }

        public bool UpdateTimeoffRecord(ApprovalDto approval)
        {
            ApprovalDto existingRecord = _approvalRepository.GetTimeoffById(approval.ApprovalId);
            if (existingRecord is null)
            {
                return false;
            }

            // update approval record
            approval.Period = existingRecord.Period;
            bool updateTimeoffRecord = _approvalRepository.UpdateTimeoffRecord(approval);

            // update timesheet records, first delete existing ones and after create new ones
            _timesheetDataRepository.DeleteTimesheetOffRecords(
                existingRecord.TimeOffId.Value,
                existingRecord.StartDate,
                existingRecord.EndDate);
            bool timeoffResponse = CreateTimeoffRecords(approval);

            return updateTimeoffRecord && timeoffResponse;
        }

        public bool DeleteTimeoffRecords(int requestId)
        {
            ApprovalDto timeoff = _approvalRepository.GetTimeoffById(requestId);
            if (timeoff is null)
            {
                return false;
            }

            // delete timeoff record
            _approvalRepository.DeleteTimeoffRecord(timeoff);

            // delete timesheet records
            _timesheetDataRepository.DeleteTimesheetOffRecords(
                    timeoff.TimeOffId.Value,
                    timeoff.StartDate.Date,
                    timeoff.EndDate.Date);

            return true;
        }

        public IEnumerable<ApprovalTimeoffRequestDto> GetTimeOffRequests(DateTime startDate, DateTime endDate, int userId)
        {
            return _approvalRepository.GetTimeoffRequests(startDate, endDate, userId);
        }

        public IEnumerable<ApprovalDto> GetRegularTimeRequests(DateTime startDate, DateTime endDate, int userId = 0)
        {
            return _approvalRepository.GetRegularTimeRequests(startDate, endDate, userId);
        }

        public bool ApproveTimeoffRequests(int[] ids, bool isWTF = true)
        {
            // get timeoff requests
            var timeoffRequests = _approvalRepository.GetApprovalRecords(ids);

            // update timeoff requests
            bool response = _approvalRepository.ProcessRequests(ids, isWTF 
                ? (int)ApprovalStatusOption.Approved
                : (int)ApprovalStatusOption.SupervisorApproval);

            if (isWTF)
            {
                // update timesheet records
                foreach (var item in timeoffRequests)
                {
                    _timesheetDataRepository.UpdateTimesheetRecords(
                        item.TimeOffId.Value,
                        item.StartDate.Date,
                        item.EndDate.Date,
                        (int)ApprovalStatusOption.Approved);
                }
            }
            
            return true;
        }

        public bool ReopenTimeoffRequests(int[] ids)
        {
            // get timeoff requests
            var timeoffRequests = _approvalRepository.GetApprovalRecords(ids);

            // update timeoff requests
            bool response = _approvalRepository.ProcessRequests(ids, (int)ApprovalStatusOption.NotSubmitted);

            // update timesheet records
            foreach (var item in timeoffRequests)
            {
                _timesheetDataRepository.UpdateTimesheetRecords(
                    item.TimeOffId.Value,
                    item.StartDate.Date,
                    item.EndDate.Date,
                    (int)ApprovalStatusOption.NotSubmitted);
            }

            return true;
        }

        public bool RejectTimeoffSelected(int[] ids)
        {
            // get timeoff requests
            var timeoffRequests = _approvalRepository.GetApprovalRecords(ids);

            // update timeoff requests
            bool response = _approvalRepository.ProcessRequests(ids, (int)ApprovalStatusOption.Rejected);

            // update timesheet records
            foreach (var item in timeoffRequests)
            {
                _timesheetDataRepository.UpdateTimesheetRecords(
                    item.TimeOffId.Value,
                    item.StartDate.Date,
                    item.EndDate.Date,
                    (int)ApprovalStatusOption.Rejected);
            }

            return true;
        }
        
        public bool RejectTimesheetsRequests(int[] ids)
        {
            // get timeoff requests
            var timeoffRequests = _approvalRepository.GetApprovalRecords(ids);

            // update timeoff requests
            bool response = _approvalRepository.ProcessRequests(ids, (int)ApprovalStatusOption.Rejected);

            // update timesheet records
            foreach (var item in timeoffRequests)
            {              

                _timesheetDataRepository.UpdateRegularRecords(
                    item.UserId,
                    (DateTime)item.Period,
                    (int)ApprovalStatusOption.Rejected);
            }

            // update timesheetcontrol records
            foreach (var item in timeoffRequests)
            {

                _timesheetControlRepository.UpdateTimesheetContolRecords(
                    item.UserId,
                    (DateTime)item.Period,
                    (int)ApprovalStatusOption.Rejected);
            }

            return true;
        }

        public bool DeleteTimeoffRequests(int[] ids)
        {
            // get timeoff requests
            var timeoffRequests = _approvalRepository.GetApprovalRecords(ids);

            // delete timeoff requests
            _approvalRepository.DeleteApprovalRequests(ids);

            // delete timesheet records
            foreach (var item in timeoffRequests)
            {
                _timesheetDataRepository.DeleteTimesheetOffRecords(item.TimeOffId.Value, item.StartDate.Date, item.EndDate.Date);
            }

            return true;
        }

        #region Private Methods
        private bool ValidateDateRange(DateTime period, DateTime startDate, DateTime endDate, decimal duration)
        {
            bool isValidRange = true;
            List<ApprovalDto> records = new();
            records.AddRange(_approvalRepository.GetTimeOffRecords(period));
            int year = period.Month == 12 ? (period.Year + 1) : period.Year;
            int month = period.Month == 12 ? 1 : (period.Month + 1);

            DateTime nextPeriod = period.Day == 16
                ? new DateTime(year, month, 1)
                : new DateTime(year, month, 16);

            records.AddRange(_approvalRepository.GetTimeOffRecords(nextPeriod));

            var groupedRecords = records.GroupBy(x => new
            {
                x.StartDate,
                x.EndDate
            })
            .Select(xg => new
            {
                xg.Key.StartDate,
                xg.Key.EndDate,
                TotalHours = xg.Sum(y => y.Duration.Value)
            });

            foreach (var record in groupedRecords)
            {
                bool validStartDate = (startDate.Date >= record.StartDate && startDate.Date <= record.EndDate);
                bool validEndDate = (endDate.Date >= record.StartDate && endDate.Date <= record.EndDate);
                bool validHours = (record.TotalHours + duration) > 8;

                if ((validStartDate || validEndDate) && validHours)
                {
                    isValidRange = false;
                    break;
                }
            }

            return isValidRange;
        }

        private bool CreateTimeoffRecords(ApprovalDto approvalRequest)
        {
            UserDto user = _userRepository.GetUser(approvalRequest.UserId);
            TimeOffDto timeOff = _timeOffRepository.GetTimeOffById(approvalRequest.UserId);
            DepartmentDto department = _departmentRepository.GetDepartmentById(approvalRequest.UserId);
            EmployeeTypeDto employeeType = user.EmpTypeId.HasValue ? _employeeTypeRepository.GetEmployeeTypeById(user.EmpTypeId.Value) : null;

            List<TimesheetData> timeoffRecords = new List<TimesheetData>();
            int days = approvalRequest.EndDate.Date.Subtract(approvalRequest.StartDate.Date).Days + 1;
            DateTime dt = approvalRequest.StartDate.Date;
            for (int i = 0; i < days; i++)
            {
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    dt = dt.AddDays(1);
                    continue;
                }

                timeoffRecords.Add(new TimesheetData
                {
                    Timesheetperiod = DateFunctions.GetPeriodStartDate(dt.Date),
                    Startdate = DateFunctions.GetPeriodStartDate(dt.Date),
                    Enddate = DateFunctions.GetPeriodLastDate(dt),
                    Entrydate = dt,
                    Userid = approvalRequest.UserId,
                    DepartmentId = department?.Id,
                    Supervisorid = user?.SupervisorId,
                    BillingpercentageRatio = 0,
                    Tasktimeoffid = approvalRequest.TimeOffId,
                    Tasktimeoffname = timeOff?.TimeOffName,
                    Tasktimeoffcode = timeOff?.TimeOffCode,
                    NonBillableHours = 0,
                    BillableHours = 0,
                    TimeOffHours = approvalRequest.Duration,
                    ProjectHours = 0,
                    TotalHours = approvalRequest.Duration,
                    Comments = approvalRequest.Comments,
                    Approvalstatus = approvalRequest.ApprovalStatusId,
                    Rpinfo1 = string.Empty,
                    Rpinfo2 = $"{dt.Year}-{dt.Month}",
                    Rpinfo3 = DateFunctions.GetDatePeriod(dt),
                    Taskname1 = timeOff?.TimeOffName,
                    Fulltaskname = timeOff?.TimeOffName,
                    Cellcomments = approvalRequest.Comments,
                    Billingpercentage = 0,
                    Username = user?.Username,
                    Email = user?.Email,
                    Employeeid = user?.Id.ToString(),
                    Userinfo1 = 0.ToString(),
                    Userinfo2 = user?.JobTitle,
                    Userinfo3 = user?.ReportsTo,
                    Loginname = user?.LoginName,
                    Employeetypeid = user?.EmpTypeId.ToString(),
                    Employeetypename = employeeType?.Employeetypename,
                    Supervisorname = user?.ReportsTo,
                    DepId = user?.DepartmentId,
                    Departmentname = department?.Name,
                    Departmentcode = department?.Code,
                });
                dt = dt.AddDays(1);
            }

            // save timeoff records
            return _timesheetDataRepository.SaveTimeOffRecords(timeoffRecords);
        }

        private static void SetRegularTimeRequest(ApprovalDto approvalRequest, int userId)
        {
            approvalRequest.TimeOffId = null;
            approvalRequest.ApprovalType = (int)ApprovalType.RegularTime;
            approvalRequest.UserId = userId;
            approvalRequest.ApprovalStatusId = (int)ApprovalStatusOption.Waiting;
        }

        private static void SetTimeoffRequest(ApprovalDto approvalRequest, int userId)
        {
            approvalRequest.ApprovalType = (int)ApprovalType.Timeoff;
            approvalRequest.UserId = userId;
            approvalRequest.ApprovalStatusId = (int)ApprovalStatusOption.Waiting;
        }

        private static bool ValidateRegularTimeRecords(int userTemplate, IEnumerable<TimesheetDataDto> records)
        {
            var groupedDays = records
            .Where(x => x.TotalHours > 0)
            .GroupBy(x => x.EntryDate)
            .Select(xg => new
            {
                xg.Key.Day,
                TotalHours = xg.Sum(y => y.TotalHours)
            })
            .ToList();


            bool areValidRecords = true;
            switch (userTemplate)
            {
                case 1:
                    const int OneDayShift = 1;
                    areValidRecords = groupedDays.ToList().TrueForAll(x => x.TotalHours == OneDayShift);
                    break;
                case 2:
                    const int EightHoursShift = 8;
                    areValidRecords = groupedDays.ToList().TrueForAll(x => x.TotalHours >= EightHoursShift);
                    break;
                default:
                    break;
            }

            return areValidRecords;
        }
        #endregion
    }

}
