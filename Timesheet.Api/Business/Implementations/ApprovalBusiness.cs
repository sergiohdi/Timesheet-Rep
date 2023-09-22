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

        public ApprovalBusiness(
            IApprovalRepository approvalRepository,
            ITimeOffRepository timeOffRepository,
            IDepartmentRepository departmentRepository,
            IUserRepository userRepository,
            IEmployeeTypeRepository employeeTypeRepository,
            ITimesheetDataRepository timesheetDataRepository
        )
        {
            _approvalRepository = approvalRepository;
            _timeOffRepository = timeOffRepository;
            _departmentRepository = departmentRepository;
            _userRepository = userRepository;
            _employeeTypeRepository = employeeTypeRepository;
            _timesheetDataRepository = timesheetDataRepository;
        }

        public bool SaveTimeOffRequest(ApprovalDto approvalRequest)
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

            bool response = _approvalRepository.SaveApprovalRequest(approvalRequest);
            bool timeOffResponse = CreateTimeoffRecords(approvalRequest);

            return response && timeOffResponse;
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

        public IEnumerable<ApprovalDto> GetTimeOffRequests(DateTime startDate, DateTime endDate, int userId = 0)
        {
            return _approvalRepository.GetTimeoffRequests(startDate, endDate, userId);
        }

        private bool ValidateDateRange(DateTime period, DateTime startDate, DateTime endDate, decimal duration)
        {
            bool isValidRange = true;
            List<ApprovalDto> records = new();
            records.AddRange(_approvalRepository.GetTimeOffRecords(period));

            DateTime nextPeriod = period.Day == 16
                ? new DateTime(period.Year, (period.Month + 1), 1)
                : new DateTime(period.Year, period.Month, 16);

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

            List<RpTimeSheetData> timeoffRecords = new List<RpTimeSheetData>();
            int days = approvalRequest.EndDate.Date.Subtract(approvalRequest.StartDate.Date).Days + 1;
            DateTime dt = approvalRequest.StartDate.Date;
            for (int i = 0; i < days; i++)
            {
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    dt = dt.AddDays(1);
                    continue;
                }

                timeoffRecords.Add(new RpTimeSheetData
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

    }

}
