using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Api.Utils;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Business.Implementations;

public class TimesheetDataBusiness : ITimesheetDataBusiness
{
    private readonly ITimesheetDataRepository _timesheetDataRepository;
    private readonly IUserSelectActRepository _userSelectActRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IActivityRepository _activityRepository;
    private readonly ITimeOffRepository _timeOffRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IEmployeeTypeRepository _employeeTypeRepository;

    public TimesheetDataBusiness(
        ITimesheetDataRepository timesheetDataRepository,
        IUserSelectActRepository userSelectActRepository,
        IClientRepository clientRepository,
        IProjectRepository projectRepository,
        IActivityRepository activityRepository,
        ITimeOffRepository timeOffRepository,
        IUserRepository userRepository,
        IDepartmentRepository departmentRepository,
        IEmployeeTypeRepository employeeTypeRepository
        )
    {
        _timesheetDataRepository = timesheetDataRepository;
        _userSelectActRepository = userSelectActRepository;
        _clientRepository = clientRepository;
        _projectRepository = projectRepository;
        _activityRepository = activityRepository;
        _timeOffRepository = timeOffRepository;
        _userRepository = userRepository;
        _departmentRepository = departmentRepository;
        _employeeTypeRepository = employeeTypeRepository;
    }

    public IEnumerable<TimesheetItemDto> GetTimesheetData(TimesheetRequestDto request)
    {
        long currentPeriod = DateFunctions.GetPeriodInTicks(
            DateTime.Now.Year,
            DateTime.Now.Month,
            DateTime.Now.Day > 15 ? 16 : 1);
        long requestPeriod = DateFunctions.GetPeriodInTicks(
            request.Year,
            request.Month,
            request.Period == 2 ? 16 : 1);

        IEnumerable<TimesheetItemDto> preferences = GetUserPreferences(request.UserId);
        IEnumerable<TimesheetDataDto> result = _timesheetDataRepository.GetTimesheetData(request);
        IEnumerable<ClientLightDto> clients = _clientRepository.GetClientsForDropDown();
        IEnumerable<ProjectLightDto> projects = _projectRepository.GetProjectsForDropDown();
        IEnumerable<ActivityDto> activities = _activityRepository.GetActivities(null);

        List<TimesheetItemDto> records;
        if (result.Any() && requestPeriod >= currentPeriod)
        {
            records = GroupTimesheetData(
                result,
                clients,
                projects,
                activities)
                .ToList();
            preferences.ToList().ForEach(item =>
                    {
                        TimesheetItemDto record = records.Find(
                    x => (x.ClientId != null && x.ClientId == item.ClientId &&
                                                x.ProjectId != null && x.ProjectId == item.ProjectId &&
                                                x.ActivityId != null && x.ActivityId == item.ActivityId));

                        if (record is null)
                        {
                            records.Add(item);
                        }
                        else
                        {
                            record.Id = item.Id;
                        }
                    });
        }
        else if (result.Any() && requestPeriod < currentPeriod)
        {
            records = GroupTimesheetData(
                result,
                clients,
                projects,
                activities)
                .ToList();
        }
        else
        {
            records = preferences.ToList();
        }

        int startDay = request.Period == 1 ? 1 : 16;
        int endDay = request.Period == 1 ? 15 : DateFunctions.GetPeriodLastDate(request.Year, request.Month, startDay).Day;

        List<TimesheetEntryDto> entries = new();
        for (int i = startDay; i <= endDay; i++)
        {
            entries.Add(new TimesheetEntryDto
            {
                Day = i,
                Comments = string.Empty,
                EntryDate = new DateTime(request.Year, request.Month, i)
            });
        }

        records.ForEach(record =>
        {
            record.Entries.AddRange(
            entries.Where(x => !record.Entries.Any(y => y.Day == x.Day)).ToList()
            );
            record.Entries = record.Entries.OrderBy(x => x.Day).ToList();
        });

        return records;
    }

    public void UpdateTimesheetBaseInformation(TimesheetItemDto record, Property property)
    {
        long timesheetId = record.Entries.FirstOrDefault().Id;
        var oldRecord = _timesheetDataRepository.GetTimesheetRecord(timesheetId);
        Dictionary<Property, object> propertyData = new Dictionary<Property, object>();
        switch (property)
        {
            case Property.Project:
                if (record.ClientId != oldRecord.ClientId)
                {
                    propertyData.Add(Property.Client, _clientRepository.GetClientById(record.ClientId.Value));
                }
                propertyData.Add(Property.Project, _projectRepository.GetProjectById(record.ProjectId.Value));
                break;
            case Property.Activity:
                propertyData.Add(Property.Activity, _activityRepository.GetActivityById(record.ActivityId.Value));
                break;
            default:
                break;
        }

        _timesheetDataRepository.UpdateTimesheetBaseInformation(record, propertyData, oldRecord.Billable.Value);
    }

    public void DeleteTimesheetRecords(TimesheetItemDto record)
    {
        long[] ids = record.Entries.Select(x => x.Id).ToArray();
        _timesheetDataRepository.DeleteTimesheetRecords(ids);
    }

    public long UpdateTimesheetHours(int userId, TimesheetItemDto record)
    {
        if (record is null)
        {
            return 0;
        }

        Dictionary<string, object> complementaryData = new Dictionary<string, object>
        {
            { "userid", userId },
            { "record", record }
        };

        TimesheetEntryDto entry = record.Entries.FirstOrDefault();
        if (entry is null)
        {
            return 0;
        }

        // If Id is different to 0 and TotalHours is 0, then delete the record
        if (entry.Id != 0 && entry.TotalHours == 0)
        {
            _timesheetDataRepository.DeleteTimesheetRecord(entry.Id);
            return 0;
        }

        if (entry.Id == 0)
        {
            ClientDto client = _clientRepository.GetClientById(record.ClientId.Value);
            ProjectDto project = _projectRepository.GetProjectById(record.ProjectId.Value);
            ActivityDto activity = _activityRepository.GetActivityById(record.ActivityId.Value);
            UserDto user = _userRepository.GetUser(userId);
            TimeOffDto timeOff = record.TimeOffId.HasValue ? _timeOffRepository.GetTimeOffById(record.TimeOffId.Value) : null;
            DepartmentDto department = user.DepartmentId.HasValue ? _departmentRepository.GetDepartmentById(user.DepartmentId.Value) : null;
            EmployeeTypeDto employeeType = user.EmpTypeId.HasValue ? _employeeTypeRepository.GetEmployeeTypeById(user.EmpTypeId.Value) : null;

            complementaryData["client"] = client;
            complementaryData["project"] = project;
            complementaryData["user"] = user;
            complementaryData["activity"] = activity;
            complementaryData["timeoff"] = timeOff;
            complementaryData["department"] = department;
            complementaryData["employeetype"] = employeeType;
        }

        return _timesheetDataRepository.SetTimesheetRecord(complementaryData);
    }

    private static IEnumerable<TimesheetItemDto> GroupTimesheetData(
        IEnumerable<TimesheetDataDto> timesheetData,
        IEnumerable<ClientLightDto> clients,
        IEnumerable<ProjectLightDto> projects,
        IEnumerable<ActivityDto> activities
    )
    {
        return timesheetData.GroupBy(x => new
        {
            x.ClientId,
            x.ProjectId,
            x.ActivityId,
            x.TimeOffId,
            x.ApprovalStatus,
            x.Billable,
            x.Location
        })
        .Select(con => new TimesheetItemDto
        {
            ClientId = con.Key.ClientId,
            ClientName = con.Key.ClientId is not null
                ? clients.FirstOrDefault(x => x.Id == con.Key.ClientId).Name
                : string.Empty,
            ProjectId = con.Key.ProjectId,
            ProjectName = con.Key.ProjectId is not null
                ? projects.FirstOrDefault(x => x.Id == con.Key.ProjectId).Name
                : string.Empty,
            ActivityId = con.Key.ActivityId,
            ActivityName = con.Key.ActivityId is not null
                ? activities.FirstOrDefault(x => x.ActivityId == con.Key.ActivityId).ActivityName
                : string.Empty,
            TimeOffId = con.Key.TimeOffId,
            ApprovalStatus = con.Key.ApprovalStatus,
            Billable = con.Key.Billable,
            Location = con.Key.Location,
            PONumber = con.FirstOrDefault()?.PONumber,
            Entries = con.Select(y => new TimesheetEntryDto
            {
                Id = y.TimesheetId,
                EntryDate = y.EntryDate,
                Day = y.EntryDate.Day,
                TotalHours = y.TotalHours,
                Comments = y.Comments
            })
            .OrderBy(y => y.EntryDate)
            .ToList()
        }).ToList();
    }

    private IEnumerable<TimesheetItemDto> GetUserPreferences(int userId)
    {
        UserSelectActDto userPreferences = _userSelectActRepository.GetUserPreferences(userId);
        IEnumerable<TimesheetItemDto> preferences = userPreferences is not null
            ? JsonConvert.DeserializeObject<IEnumerable<TimesheetItemDto>>(userPreferences.Activities)
            : new List<TimesheetItemDto>();

        foreach (TimesheetItemDto entry in preferences)
        {
            entry.Entries = new List<TimesheetEntryDto>();
            if (entry.IsTimeOff)
            {
                continue;
            }

            entry.Location = "Office";
        }

        return preferences;
    }
}
