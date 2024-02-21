using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Client.Extensions;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;
using Timesheet.Shared.Utils;

namespace Timesheet.Client.Services.Implementations;

public class TimesheetService : ITimesheetService
{
    private readonly ITimesheetControlDataService _timesheetControlDataSrv;
    private readonly ITimesheetDataService _timesheetDataSvc;
    private readonly IApprovalHistoryService _approvalHistoryService;

    public TimesheetService(ITimesheetControlDataService timesheetControlDataSrv, 
                            ITimesheetDataService timesheetDataSvc,
                            IApprovalHistoryService approvalHistoryService
        )
    {
        _timesheetControlDataSrv = timesheetControlDataSrv;
        _timesheetDataSvc = timesheetDataSvc;
        _approvalHistoryService = approvalHistoryService;
    }

    public DateTime StartPeriod { get; private set; }
    public DateTime EndPeriod { get; private set; }
    public List<TimesheetItem> TimesheetData { get; private set; } = new();
    public TimesheetControl TimesheetControl { get; private set; }
    public Dictionary<DateTime, decimal> TotalHoursPerDay { get; private set; } = new();
    public List<GetApprovalsHistory> ApprovalsHistory { get; private set; } = new();
    public List<TimeEntryDetails> TimeEntryDetails { get; private set; } = new();

    public void SetStartAndEndPeriod(DateTime date)
    {
        StartPeriod = date;
        EndPeriod = DateFunctions.GetPeriodLastDate(date);
    }

    public async Task GetTimesheetRecords(int userId, int year, int month, int period)
    {
        var result = await _timesheetDataSvc.GetTimesheetDataByUserAsync(
            userId,
            year,
            month,
            period
        );

        if (result.Status == ResponseStatus.Success) 
        {
            TimesheetData = result.Data.ToList();
            SetRegularEntryDetails();
        }
    }

    public async Task GetTimesheetControlRecord(DateTime period)
    {
        TimesheetControl = (await _timesheetControlDataSrv.GetTimesheetControlRecord(period)).Data;
    }

    public async Task CreateTimesheetControl(DateTime period)
    {
        TimesheetControl = (await _timesheetControlDataSrv.CreateTimesheetControl(period)).Data;
    }

    public void CalculateHoursPerColumn(DateTime startDate, DateTime endDate)
    {
        // make calculations to see total hours per day and per row
        TotalHoursPerDay = new Dictionary<DateTime, decimal>();
        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
        {
            decimal totalHours = TimesheetData.Sum(x => x.Entries.FirstOrDefault(y => y.Day == date.Day)?.TotalHours) ?? 0;
            TotalHoursPerDay.Add(date, totalHours);
        }
    }

    public List<decimal> GetOneTemplateValues()
    {
        Dictionary<int, decimal> oneDayValue = new();
        TimesheetData.ForEach(x => x.Entries.ForEach(y =>
        {
            if (oneDayValue.ContainsKey(y.Day))
            {
                oneDayValue[y.Day] += y.TotalHours;
            }
            else
            {
                oneDayValue.Add(y.Day, y.TotalHours);
            }
        }));

        return oneDayValue.Values.ToList();
    }

    public List<decimal> GetWeekDaysHours()
    {
        List<decimal> hoursPerDay = new();
        foreach (var item in TotalHoursPerDay)
        {
            DateTime key = item.Key;
            if (key.DayOfWeek == DayOfWeek.Saturday || key.DayOfWeek == DayOfWeek.Sunday)
            {
                continue;
            }

            hoursPerDay.Add(item.Value);
        }

        return hoursPerDay;
    }

    public bool PeriodGetNegativeValues()
        => TotalHoursPerDay.Any(x => x.Value < 0);

    public void ReleaseData()
    {
        StartPeriod = DateTime.MinValue;
        EndPeriod = DateTime.MinValue;
        TimesheetControl = null;
        TimesheetData = new();
        ApprovalsHistory = new();
        TotalHoursPerDay = new();
        TimeEntryDetails = new();
    }

    public async Task GetApprovalsHistory()
    {
        var response = await _approvalHistoryService.GetApprovalsHistory(TimesheetControl.TimesheetPeriodId);
        if (response.Status == ResponseStatus.Success)
        {
            ApprovalsHistory = response.Data;
        }
    }

    private void SetRegularEntryDetails()
    {
        TimeEntryDetails = new();

        TimesheetData.Where(x => x.IsTimeOff == false)
            .ToList()
            .ForEach(record =>
            {
                record.Entries
                .Where(y => y.TotalHours > 0)
                .ToList()
                .ForEach(entry =>
                {
                    TimeEntryDetails.Add(new TimeEntryDetails
                    {
                        Date = entry.EntryDate,
                        DateEntry = entry.EntryDate.ToString("MMM dd, yyyy"),
                        ClientProject = $"{ record.ClientName } - { record.ProjectName }",
                        Task = "No Task",
                        Activity = record.ActivityName,
                        Time = entry.TotalHours.GetDecimalWithFormat(Constants.decimalFormat),
                        Comments = entry.Comments.Trim(),
                        Location = record.Location
                    });
                });
            });

        TimeEntryDetails = TimeEntryDetails.OrderBy(x => x.Date).ToList();
    }

}
