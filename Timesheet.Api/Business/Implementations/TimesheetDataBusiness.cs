using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Api.Utils;

namespace Timesheet.Api.Business.Implementations
{
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
            string currentDatePeriod = GetCurrentDatePeriod();
            string requestPeriod = $"{request.Year}-{request.Month}-{request.Period}";
            IEnumerable<TimesheetItemDto> preferences = GetUserPreferences(request.UserId);
            IEnumerable<TimesheetDataDto> result = _timesheetDataRepository.GetTimesheetData(request);

            List<TimesheetItemDto> records;
            if (result.Any() && currentDatePeriod.Equals(requestPeriod))
            {
                records = GroupTimesheetData(result).ToList();
                preferences.ToList().ForEach(item =>
                {
                    TimesheetItemDto entry = records.FirstOrDefault(
                        x => (x.ClientId != null && x.ClientId == item.ClientId &&
                                                    x.ProjectId != null && x.ProjectId == item.ProjectId &&
                                                    x.ActivityId != null && x.ActivityId == item.ActivityId));

                    if (entry is null)
                    {
                        records.Add(item);
                    }
                    else
                    {
                        entry.Id = item.Id;
                    }
                });
            }
            else if (result.Any() && !currentDatePeriod.Equals(requestPeriod))
            {
                records = GroupTimesheetData(result).ToList();
            }
            else
            {
                records = preferences.ToList();
            }

            int startDay = request.Period == 1 ? 1 : 16;
            int endDay = request.Period == 1 ? 15 : new DateTime(request.Year, (request.Month + 1), 1).AddDays(-1).Day;
            List<TimesheetEntryDto> entries = new();
            for (int i = startDay; i <= endDay; i++)
            {
                entries.Add(new TimesheetEntryDto
                {
                    Day = i,
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
            int oldBillableInfo = _timesheetDataRepository.GetTimesheetRecord(record.Entries.FirstOrDefault().Id)?.Billable.Value ?? 1;
            object propertyData = null;
            switch (property)
            {
                case Property.Client:
                    propertyData = _clientRepository.GetClientById(record.ClientId.Value);
                    break;
                case Property.Project:
                    propertyData = _projectRepository.GetProjectById(record.ProjectId.Value);
                    break;
                case Property.Activity:
                    propertyData = _activityRepository.GetActivityById(record.ActivityId.Value);
                    break;
                default:
                    break;
            }
            
            _timesheetDataRepository.UpdateTimesheetBaseInformation(record, property, propertyData, oldBillableInfo);
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

                // If the record is deleted, then return -1
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

        private static IEnumerable<TimesheetItemDto> GroupTimesheetData(IEnumerable<TimesheetDataDto> timesheetData)
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
                ProjectId = con.Key.ProjectId,
                ActivityId = con.Key.ActivityId,
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

        private static string GetCurrentDatePeriod()
        {
            string currentPeriod = DateTime.Now.Day > 15 ? "2" : "1";
            return $"{DateTime.Now.Year}-{DateTime.Now.Month}-{currentPeriod}";
        }
    }
}
