using System.Collections.Generic;
using System.Linq;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Shared.Utils;

namespace Timesheet.Client.Services.Implementations;

public class TimesheetValidationsService : ITimesheetValidationsService
{
    private readonly ITimesheetService _timesheetService;

    public TimesheetValidationsService(ITimesheetService timesheetService)
    {
        _timesheetService = timesheetService;
    }

    public List<string> ValidateTimesheet()
    {
        List<string> errors = new()
        {
            ValidateUserTemplate(),
            // This is an example calling multiple validation functions in the validation flow
            // OtherValidation()
        };

        return errors.Where(error => !string.IsNullOrEmpty(error)).ToList();
    }

    private string ValidateUserTemplate()
    {
        Dictionary<int, string> errors = new()
        {
            { 1, "Ensure that all registered days have a value of 1 as a valid value and also negative values are not allowed." },
            { 2, "Ensure that all registered days must have 8 or more as a valid value and also negative values are not allowed." }
        };

        int result = (int)DayValuesError.NoError;
        int userTemplateId = _timesheetService.TimesheetControl.UserTemplateId;
        bool periodHasNegativeValues = _timesheetService.PeriodGetNegativeValues();

        if (userTemplateId == (int)UserTimesheetTemplate.DailyShift)
        {
            const int OneDayShift = 1;
            List<decimal> values = _timesheetService.GetOneTemplateValues();
            bool validDays = values.TrueForAll(n => n == OneDayShift);
            result = !periodHasNegativeValues && validDays
                ? (int)DayValuesError.NoError
                : (int)DayValuesError.OneDayShiftError;
        }
        else if (userTemplateId == (int)UserTimesheetTemplate.EightHoursShift)
        {
            const int EightHoursShift = 8;
            List<decimal> values = _timesheetService.GetWeekDaysHours();
            bool validDays = values.TrueForAll(n => n >= EightHoursShift);
            result = !periodHasNegativeValues && validDays
                ? (int)DayValuesError.NoError
                : (int)DayValuesError.EightHoursShiftError;
        }

        return errors.TryGetValue(result, out string message) ? message : string.Empty;
    }

    private static string OtherValidation()
    {
        return "This is another validation that fails too!!!";
    }
}
