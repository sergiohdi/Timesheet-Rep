using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Api.Utils;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Repositories.EF_Implementations
{
    public class TimesheetDataRepository : ITimesheetDataRepository
    {
        private readonly TimesheetContext _db;
        private readonly IMapper _mapper;

        public TimesheetDataRepository(TimesheetContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IEnumerable<TimesheetDataDto> GetTimesheetData(TimesheetRequestDto request)
        {
            try
            {
                int day = request.Period == 2 ? 16 : 1;
                DateTime dateFilter = new(request.Year, request.Month, day);

                return _db.RpTimeSheetData.AsNoTracking()
                                          .Where(x => x.Userid == request.UserId && x.Timesheetperiod == dateFilter)
                                          .Select(x => new TimesheetDataDto
                                          {
                                              // TimesheetId = x.TimesheetId,
                                              ClientId = x.Clientid.Value,
                                              ProjectId = x.Projectid.Value,
                                              ActivityId = x.ActivityId.Value,
                                              TimeOffId = x.Tasktimeoffid.Value,
                                              EntryDate = x.Entrydate,
                                              TotalHours = x.TotalHours.Value,
                                              Comments = x.Comments,
                                              ApprovalStatus = x.Approvalstatus.Value,
                                              Billable = x.Billable.Value,
                                              Location = x.Ttinfo2,
                                              PONumber = x.Ttinfo3 ?? string.Empty
                                          })
                                          .ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public TimesheetDataDto GetTimesheetRecord(long timesheetId)
        {
            return _mapper.Map<TimesheetDataDto>(_db.RpTimeSheetData.FirstOrDefault(x => x.TimesheetId == timesheetId));
        }

        public void UpdateTimesheetBaseInformation(TimesheetItemDto item, Property property, object propertyData, int oldBillableInfo)
        {
            long[] ids = item.Entries.Select(x => x.Id).ToArray();
            int billable = item.Billable == 9 ? 1 : 0;
            StringBuilder strQuery = new();
            strQuery.Append(@$"UPDATE [dbo].[RP_TimeSheetData] SET ttinfo3 = '{item.PONumber}', billable = {billable}, ttinfo2 = '{item.Location}'");

            if (property == Property.Client)
            {
                ClientDto client = (ClientDto)propertyData;
                strQuery.Append(@$", clientid = {client.Id}, clientname = '{client.Name}', clientcode = '{client.Code}'");
            }

            if (property == Property.Project)
            {
                ProjectDto project = (ProjectDto)propertyData;
                strQuery.Append(@$", projectid = {project.Id}, projectname = '{project.Name}', projectcode = '{project.ProjectCode}', taskname1 = '{project.Name}', fulltaskname = '{project.Name}'");
            }

            if (property == Property.Activity)
            {
                ActivityDto activity = (ActivityDto)propertyData;
                strQuery.Append(@$", ActivityId = {activity.ActivityId}, ActivityName = '{activity.ActivityName}', ActivityCode = '{activity.ActivityCode}'");
            }

            if (billable != oldBillableInfo && item.Billable == 9)
            {
                strQuery.Append($@", billable_hours = total_hours, non_billable_hours = NULL");
            }
            else if (billable != oldBillableInfo && item.Billable == 10)
            {
                strQuery.Append($@", non_billable_hours = total_hours, billable_hours = NULL");
            }

            strQuery.Append(@$" WHERE timesheetid IN ({string.Join(",", ids.Where(id => id != 0))})");

            string asd = strQuery.ToString();

            _db.Database.ExecuteSqlRaw(strQuery.ToString());
        }

        public void DeleteTimesheetRecords(long[] ids)
        {
            _db.RpTimeSheetData.Where(x => ids.Contains(x.TimesheetId))
                               .ExecuteDelete();
        }

        public long SetTimesheetRecord(Dictionary<string, object> data)
        {
            int userId = data["userid"] != null ? Convert.ToInt32(data["userid"]) : 0;
            if (userId == 0)
            {
                throw new Exception("User Id is required");
            }

            TimesheetItemDto record = (data["record"] != null ? (TimesheetItemDto)data["record"] : null)
                                                                ?? throw new Exception("Record is required");
            TimesheetEntryDto entry = record.Entries.FirstOrDefault() ?? throw new Exception("Entry is required");

            ClientDto client = null;
            ProjectDto project = null;
            ActivityDto activity = null;
            UserDto user = null;
            TimeOffDto timeOff = null;
            DepartmentDto department = null;
            EmployeeTypeDto employeeType = null;
            if (entry.Id == 0)
            {
                client = data["client"] != null ? (ClientDto)data["client"] : null;
                project = data["project"] != null ? (ProjectDto)data["project"] : null;
                activity = data["activity"] != null ? (ActivityDto)data["activity"] : null;
                user = data["user"] != null ? (UserDto)data["user"] : null;
                timeOff = data["timeoff"] != null ? (TimeOffDto)data["timeoff"] : null;
                department = data["department"] != null ? (DepartmentDto)data["department"] : null;
                employeeType = data["employeetype"] != null ? (EmployeeTypeDto)data["employeetype"] : null;
            }

            RpTimeSheetData timesheetRecord = entry.Id > 0
                ? _db.RpTimeSheetData.FirstOrDefault(x => x.TimesheetId == entry.Id)
                : _db.RpTimeSheetData.FirstOrDefault(x => x.Entrydate == entry.EntryDate &&
                                                                      x.Userid == userId &&
                                                                      x.Clientid == record.ClientId &&
                                                                      x.Projectid == record.ProjectId &&
                                                                      x.ActivityId == record.ActivityId);

            if (timesheetRecord != null)
            {
                timesheetRecord.TotalHours = entry.TotalHours;
                timesheetRecord.Comments = entry.Comments;
                timesheetRecord.Cellcomments = entry.Comments;
            }
            else
            {
                DateTime startDate = DateFunctions.GetPeriodStartDate(entry.EntryDate.Year, entry.EntryDate.Month, entry.Day);
                DateTime endDate = DateFunctions.GetPeriodLastDate(entry.EntryDate.Year, entry.EntryDate.Month, entry.Day);

                timesheetRecord = new RpTimeSheetData
                {
                    Timesheetperiod = startDate,
                    Startdate = startDate,
                    Enddate = endDate,
                    Entrydate = entry.EntryDate,
                    Userid = userId,
                    DepartmentId = user?.DepartmentId,
                    Supervisorid = user?.SupervisorId,
                    Projectid = record.ProjectId,
                    Projectcode = project?.ProjectCode,
                    Projectname = project?.Name,
                    Clientid = record.ClientId,
                    Tasktimeoffid = record.TimeOffId,
                    Tasktimeoffname = timeOff?.TimeOffName,
                    Htasktimeoffname = timeOff?.TimeOffCode,
                    ActivityId = record.ActivityId,
                    ActivityName = activity?.ActivityName,
                    ActivityCode = activity?.ActivityCode,
                    Billable = record.Billable,
                    NonBillableHours = record.Billable == 0 ? entry.TotalHours : null,
                    BillableHours = record.Billable == 1 ? entry.TotalHours : null,
                    TimeOffHours = record.IsTimeOff ? entry.TotalHours : null,
                    ProjectHours = entry.TotalHours,
                    TotalHours = entry.TotalHours,
                    Comments = entry.Comments,
                    Approvalstatus = 0, // Not Submited
                    Taskinfo3 = project.UdfProjectType,
                    Rpinfo1 = string.Empty, // ATTN PAYROLL
                    Rpinfo2 = $"{entry.EntryDate.Year}-{entry.EntryDate.Month}", // NewForAccountingUse
                    Rpinfo3 = entry.Day > 15 ? "2-2" : "1-2", // NewBillingCycle
                    Ttinfo2 = record.Location,
                    Ttinfo3 = record.PONumber,
                    Taskname1 = project?.Name,
                    Fulltaskname = project?.Name,
                    Cellcomments = entry.Comments,
                    Username = user?.Username,
                    Email = user?.Email,
                    Employeeid = user?.ExternalId,
                    Userinfo1 = "0", // always has value 0
                    Userinfo2 = user?.JobTitle,
                    Userinfo3 = user?.ReportsTo,
                    Loginname = user?.LoginName,
                    Employeetypeid = user?.EmpTypeId.ToString(),
                    Employeetypename = employeeType?.Employeetypename,
                    Supervisorname = user?.ReportsTo,
                    DepId = user?.DepartmentId,
                    Departmentname = department?.Name,
                    Departmentcode = department?.Code,
                    Clientname = client?.Name,
                    Clientcode = client?.Code
                };

                _db.RpTimeSheetData.Add(timesheetRecord);
            }

            _db.SaveChanges();
            return timesheetRecord.TimesheetId;
        }

        public bool ValidateTimesheetByProjectId(int projectId)
        {
            return _db.RpTimeSheetData.Any(x => x.Projectid == projectId);
        }

        public bool ValidateTimesheetByActivityId(int activityId)
        {
            return _db.RpTimeSheetData.Any(x => x.ActivityId == activityId);
        }

        public bool ValidateTimesheetByUserId(int userId)
        {
            return _db.RpTimeSheetData.Any(x => x.Userid == userId);
        }

        public void DeleteTimesheetRecord(long id)
        {
            _db.RpTimeSheetData.Where(x => x.TimesheetId == id)
                               .ExecuteDelete();
        }

        public bool SaveTimeOffRecords(IEnumerable<RpTimeSheetData> timeOffRecords) 
        {
            _db.RpTimeSheetData.AddRange(timeOffRecords);
            return _db.SaveChanges() > 0;
        }

        public void DeleteTimesheetOffRecords(int id, DateTime startDate, DateTime endDate)
        {
            _db.RpTimeSheetData.Where(
                x => x.Tasktimeoffid == id && x.Entrydate >= startDate.Date && x.Entrydate <= endDate.Date)
                .ExecuteDelete();
        }

        public bool UpdateApprovedRecords(List<DateTime> periods, List<int> userIds)
        {
            _db.RpTimeSheetData.Where(x => periods.Contains(x.Timesheetperiod) && userIds.Contains(x.Userid))
                .ExecuteUpdate(s => s.SetProperty(x => x.Approvalstatus, 2));

            return true;
        }
    }
}