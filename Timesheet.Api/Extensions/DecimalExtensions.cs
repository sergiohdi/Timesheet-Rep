using System;

namespace Timesheet.Api.Extensions;

public static class DecimalExtensions
{
    public static decimal ConvertHoursToUnits(decimal hours)
    {
        return Math.Round(hours / 8.0m, 2);
    }

    public static decimal ConvertUnitsToHours(decimal units)
    {
        return Math.Round(units * 8.0m, 1);
    }
}
