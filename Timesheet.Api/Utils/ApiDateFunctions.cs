using System;
using System.Collections.Generic;

namespace Timesheet.Api.Utils;

public static class ApiDateFunctions
{
    public static IEnumerable<DateTime> GetWeekendDates()
    {
        int currentYear = DateTime.Now.Year;
        DateTime start = new (currentYear, 1, 1, 0, 0, 0,DateTimeKind.Local);
        DateTime end = new (currentYear, 12, 31, 0, 0, 0, DateTimeKind.Local);
        List<DateTime> weekendDates = new();

        for (DateTime i = start; i < end; i = i.AddDays(1))
        {
            if (i.DayOfWeek == DayOfWeek.Sunday || i.DayOfWeek == DayOfWeek.Saturday) 
            {
                weekendDates.Add(i);
            }
        }

        return weekendDates;
    }
}
