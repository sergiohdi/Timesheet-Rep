﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces;

public interface ITimesheetService
{
    DateTime StartPeriod { get; }
    DateTime EndPeriod { get; }
    TimesheetControl TimesheetControl { get; }
    List<TimesheetItem> TimesheetData { get; }
    Dictionary<DateTime, decimal> TotalHoursPerDay { get; }
    List<GetApprovalsHistory> ApprovalsHistory { get; }
    List<TimeEntryDetails> TimeEntryDetails { get; }

    void SetStartAndEndPeriod(DateTime date);
    Task GetTimesheetControlRecord(DateTime period);
    Task GetTimesheetRecords(int userId, int year, int month, int period);
    Task CreateTimesheetControl(DateTime period);
    void CalculateHoursPerColumn();
    List<decimal> GetOneTemplateValues();
    List<decimal> GetWeekDaysHours();
    bool PeriodGetNegativeValues();
    void ReleaseData();
    Task GetApprovalsHistory();
}