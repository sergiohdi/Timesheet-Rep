using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Extensions;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Api.Utils;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Business.Implementations;

public class UserBusiness : IUserBusiness
{
    private readonly IUserRepository _userRepository;
    private readonly ITimesheetControlRepository _timesheetControlRepository;
    private readonly IApprovalRepository _approvalRepository;
    private readonly ITimesheetDataRepository _timesheetDataRepository;
    private readonly IConfiguration _configuration;

    public UserBusiness(
        IUserRepository userRepository,
        ITimesheetControlRepository timesheetControlRepository,
        IApprovalRepository approvalRepository,
        ITimesheetDataRepository timesheetDataRepository,
        IConfiguration configuration
    )
    {
        _userRepository = userRepository;
        _timesheetControlRepository = timesheetControlRepository;
        _approvalRepository = approvalRepository;
        _timesheetDataRepository = timesheetDataRepository;
        _configuration = configuration;
    }

    public IEnumerable<UserDto> GetUsers(bool? disabled) => _userRepository.GetUsers(disabled);

    public UserDto GetUser(int userId) => _userRepository.GetUser(userId);

    public bool CreateUser(UserDto user)
    {
        string password = _configuration["DefaultPassword"];
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        return _userRepository.CreateUser(user, hashedPassword);
    }

    public bool UpdateUser(UserDto user)
    {
        UserDto oldUserData = _userRepository.GetUser(user.Id);
        bool successUpdate = _userRepository.UpdateUser(user);

        if (user.TimesheetTemplate != oldUserData.TimesheetTemplate)
        {
            ChangesInUserTemplate(
                user,
                user.TimesheetTemplate.Value,
                user.TimesheetTemplate == (int)UserTimesheetTemplate.DailyShift
            );
        }

        return successUpdate;
    }

    public bool DeleteUser(int userId)
    {
        UserDto user = _userRepository.GetUser(userId);
        if (user is null)
        {
            return false;
        }

        bool userExistsOnTimesheetRecords = _timesheetDataRepository.ValidateTimesheetByUserId(userId);
        if (userExistsOnTimesheetRecords)
        {
            return false;
        }

        bool result;
        try
        {
            result = _userRepository.DeleteUser(user);
        }
        catch (System.Exception ex)
        {
            if (!ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE"))
            {
                throw;
            }

            result = false;
        }

        return result;
    }

    private void ChangesInUserTemplate(UserDto user, int newTemplateId, bool isDailyTemplate)
    {
        DateTime currentPeriod = DateFunctions.GetPeriodStartDate(DateTime.Now);
        List<TimesheetControlDto> futureTimesheetControls = _timesheetControlRepository.GetFutureTimesheetControlRecords(currentPeriod, user.Id);
        if (!futureTimesheetControls.Any())
        {
            return;
        }

        List<ApprovalDto> futureApprovals = _approvalRepository.GetFutureApprovals(currentPeriod, user.Id);
        List<TimesheetDataFutureDto> futureTimesheetsData = _timesheetDataRepository.GetFutureTimesheetDataRecords(currentPeriod, user.Id);

        // Make value conversions on approvals and timesheet data
        futureApprovals.ForEach(x =>
            x.Duration = isDailyTemplate
                ? DecimalExtensions.ConvertHoursToUnits(x.Duration.Value)
                : DecimalExtensions.ConvertUnitsToHours(x.Duration.Value)
        );

        foreach (TimesheetDataFutureDto timesheetData in futureTimesheetsData)
        {
            if (timesheetData.TimeoffId > 0)
            {
                timesheetData.TimeOffHours = isDailyTemplate
                    ? DecimalExtensions.ConvertHoursToUnits(timesheetData.TimeOffHours)
                    : DecimalExtensions.ConvertUnitsToHours(timesheetData.TimeOffHours);
                timesheetData.TotalHours = isDailyTemplate
                    ? DecimalExtensions.ConvertHoursToUnits(timesheetData.TotalHours)
                    : DecimalExtensions.ConvertUnitsToHours(timesheetData.TotalHours);
                continue;
            }

            if (timesheetData.Billable == (int)BillableOptions.Billable)
            {
                timesheetData.BillableHours = isDailyTemplate
                    ? DecimalExtensions.ConvertHoursToUnits(timesheetData.BillableHours)
                    : DecimalExtensions.ConvertUnitsToHours(timesheetData.BillableHours);
                timesheetData.ProjectHours = isDailyTemplate
                    ? DecimalExtensions.ConvertHoursToUnits(timesheetData.ProjectHours)
                    : DecimalExtensions.ConvertUnitsToHours(timesheetData.ProjectHours);
                timesheetData.TotalHours = isDailyTemplate
                    ? DecimalExtensions.ConvertHoursToUnits(timesheetData.TotalHours)
                    : DecimalExtensions.ConvertUnitsToHours(timesheetData.TotalHours);
                continue;
            }

            if (timesheetData.Billable == (int)BillableOptions.NonBillable)
            {
                timesheetData.NonBillableHours = isDailyTemplate
                    ? DecimalExtensions.ConvertHoursToUnits(timesheetData.NonBillableHours)
                    : DecimalExtensions.ConvertUnitsToHours(timesheetData.NonBillableHours);
                timesheetData.TotalHours = isDailyTemplate
                    ? DecimalExtensions.ConvertHoursToUnits(timesheetData.TotalHours)
                    : DecimalExtensions.ConvertUnitsToHours(timesheetData.TotalHours);
            }
        }

        // Update records in their respectives tables
        _timesheetControlRepository.UpdateUserTemplate(
            futureTimesheetControls.Select(x => x.TimesheetPeriodId).ToArray(), 
            newTemplateId);

        if (futureApprovals.Any()) 
        {
            _approvalRepository.UpdateApprovals(futureApprovals);
        }

        if(futureTimesheetsData.Any()) 
        {
            _timesheetDataRepository.UpdateFutureTimesheetRecords(futureTimesheetsData);
        }
    }
}
