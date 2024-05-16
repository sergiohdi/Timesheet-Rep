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

namespace Timesheet.Api.Repositories.EF_Implementations;

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
        int day = request.Period == 2 ? 16 : 1;
        DateTime dateFilter = new(request.Year, request.Month, day);

        return _db.TimesheetData.AsNoTracking()
                                  .Where(x => x.Userid == request.UserId && x.Timesheetperiod == dateFilter)
                                  .Select(x => new TimesheetDataDto
                                  {
                                      TimesheetId = x.TimesheetId,
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

    public TimesheetDataDto GetTimesheetRecord(long timesheetId)
        => _mapper.Map<TimesheetDataDto>(_db.TimesheetData.FirstOrDefault(x => x.TimesheetId == timesheetId));

    public void UpdateTimesheetBaseInformation(TimesheetItemDto item, Dictionary<Property, object> propertyData, int oldBillableInfo)
    {
        long[] ids = item.Entries.Select(x => x.Id).ToArray();
        int billable = item.Billable == 11 ? 1 : 0;
        StringBuilder strQuery = new();
        strQuery.Append(@$"UPDATE [dbo].[RP_TimeSheetData] SET ttinfo3 = '{item.PONumber}', billable = {billable}, ttinfo2 = '{item.Location}'");

        if (propertyData.ContainsKey(Property.Client))
        {
            ClientDto client = (ClientDto)propertyData[Property.Client];
            strQuery.Append(@$", clientid = {client.Id}, clientname = '{client.Name}', clientcode = '{client.Code}'");
        }

        if (propertyData.ContainsKey(Property.Project))
        {
            ProjectDto project = (ProjectDto)propertyData[Property.Project];
            strQuery.Append(@$", projectid = {project.Id}, projectname = '{project.Name}', projectcode = '{project.ProjectCode}', taskname1 = '{project.Name}', fulltaskname = '{project.Name}'");
        }

        if (propertyData.ContainsKey(Property.Activity))
        {
            ActivityDto activity = (ActivityDto)propertyData[Property.Activity];
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

        _db.Database.ExecuteSqlRaw(strQuery.ToString());
    }

    public void DeleteTimesheetRecords(long[] ids) => 
        _db.TimesheetData.Where(x => ids.Contains(x.TimesheetId)).ExecuteDelete();

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

        TimesheetData timesheetRecord = entry.Id > 0
            ? _db.TimesheetData.FirstOrDefault(x => x.TimesheetId == entry.Id)
            : _db.TimesheetData.FirstOrDefault(x => x.Entrydate == entry.EntryDate &&
                                                                  x.Userid == userId &&
                                                                  x.Clientid == record.ClientId &&
                                                                  x.Projectid == record.ProjectId &&
                                                                  x.ActivityId == record.ActivityId);

        if (timesheetRecord != null)
        {
            if (timesheetRecord.Billable == 1)
            {
                timesheetRecord.BillableHours = entry.TotalHours;
            }
            else
            {
                timesheetRecord.NonBillableHours = entry.TotalHours;
            }

            timesheetRecord.ProjectHours = entry.TotalHours;
            timesheetRecord.TotalHours = entry.TotalHours;
            timesheetRecord.Comments = entry.Comments;
            timesheetRecord.Cellcomments = entry.Comments;
        }
        else
        {
            DateTime startDate = DateFunctions.GetPeriodStartDate(entry.EntryDate.Year, entry.EntryDate.Month, entry.Day);
            DateTime endDate = DateFunctions.GetPeriodLastDate(entry.EntryDate.Year, entry.EntryDate.Month, entry.Day);

            timesheetRecord = new TimesheetData
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

            _db.TimesheetData.Add(timesheetRecord);
        }

        _db.SaveChanges();
        return timesheetRecord.TimesheetId;
    }

    public bool ValidateTimesheetByProjectId(int projectId) => _db.TimesheetData.Any(x => x.Projectid == projectId);

    public bool ValidateTimesheetByActivityId(int activityId) => _db.TimesheetData.Any(x => x.ActivityId == activityId);

    public bool ValidateTimesheetByUserId(int userId) => _db.TimesheetData.Any(x => x.Userid == userId);

    public void DeleteTimesheetRecord(long id) => _db.TimesheetData.Where(x => x.TimesheetId == id).ExecuteDelete();

    public bool SaveTimeOffRecords(IEnumerable<TimesheetData> timeOffRecords)
    {
        _db.TimesheetData.AddRange(timeOffRecords);
        return _db.SaveChanges() > 0;
    }

    public void DeleteTimesheetOffRecords(int id, int userId, DateTime startDate, DateTime endDate) => 
        _db.TimesheetData.Where(x => 
                            x.Userid == userId && 
                            x.Tasktimeoffid == id && 
                            x.Entrydate >= startDate.Date && 
                            x.Entrydate <= endDate.Date)
                         .ExecuteDelete();

    public bool UpdateTimesheetRecords(int id, int userId, DateTime startDate, DateTime endDate, int status)
    {
        _db.TimesheetData.Where(
            x => x.Userid == userId && x.Tasktimeoffid == id && x.Entrydate >= startDate.Date && x.Entrydate <= endDate.Date)
            .ExecuteUpdate(s => s.SetProperty(x => x.Approvalstatus, status));

        return true;
    }

    public bool DeleteTimesheetRecord(DateTime period, int userId)
    {
        _db.TimesheetData.Where(x => x.Timesheetperiod == period && x.Userid == userId)
            .ExecuteDelete();

        return true;
    }

    public bool UpdateRegularRecords(int id, DateTime startDate, DateTime endDate, int status)
    {
        _db.TimesheetData.Where(
            x => x.TimesheetId == id && x.Entrydate >= startDate.Date && x.Entrydate <= endDate.Date)
            .ExecuteUpdate(s => s.SetProperty(x => x.Approvalstatus, status));

        return true;
    }

    public bool UpdateRegularRecords(int userId, DateTime period, int status)
    {
        _db.TimesheetData.Where(x =>
                                                    x.Userid == userId &&
                                                    x.Timesheetperiod == period.Date &&
                                                    x.Clientid != null)
                           .ExecuteUpdate(s =>
                                    s.SetProperty(x => x.Approvalstatus, status));

        return true;
    }

    public bool DeleteTimesheetOnlyRegularTime(DateTime period, int[] userIds)
    {
        //Delete only timesheet rows excluding timeoffs            
        _db.TimesheetData.Where(x => 
            x.Timesheetperiod == period &&
            x.Tasktimeoffid == null &&
            userIds.Contains(x.Userid) )
            .ExecuteDelete();

        return true;
    }

    public IEnumerable<TimesheetDataDto> GetRegularTimeData(DateTime period, int userId)
    {
        return _db.TimesheetData.AsNoTracking()
                                  .Where(x => x.Timesheetperiod == period && x.Userid == userId && x.Clientid != null)
                                  .Select(x => new TimesheetDataDto
                                  {
                                      TimesheetId = x.TimesheetId,
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

    public List<TimesheetDataFutureDto> GetFutureTimesheetDataRecords(DateTime currentPeriod, int userId)
    {
        return _db.TimesheetData.AsNoTracking()
                                  .Where(x => x.Userid == userId && x.Timesheetperiod > currentPeriod)
                                  .Select(x => new TimesheetDataFutureDto
                                  {
                                      TimesheetId = x.TimesheetId,
                                      TimeoffId = x.Tasktimeoffid.Value,
                                      Billable = x.Billable.HasValue ? x.Billable.Value : 0,
                                      NonBillableHours = x.NonBillableHours.Value,
                                      BillableHours = x.BillableHours.Value,
                                      TimeOffHours = x.TimeOffHours.Value,
                                      ProjectHours = x.ProjectHours.Value,
                                      TotalHours = x.TotalHours.Value,
                                  })
                                  .ToList();
    }

    public bool UpdateFutureTimesheetRecords(IEnumerable<TimesheetDataFutureDto> records)
    {
        foreach (var record in records)
        {
            _db.TimesheetData.Where(x => x.TimesheetId == record.TimesheetId)
                              .ExecuteUpdate(s => 
                                                  s.SetProperty(x => x.NonBillableHours, record.NonBillableHours)
                                                   .SetProperty(x => x.BillableHours, record.BillableHours)
                                                   .SetProperty(x => x.TimeOffHours, record.TimeOffHours)
                                                   .SetProperty(x => x.ProjectHours, record.ProjectHours)
                                                   .SetProperty(x => x.TotalHours, record.TotalHours));
        }

        return true;
    }
}