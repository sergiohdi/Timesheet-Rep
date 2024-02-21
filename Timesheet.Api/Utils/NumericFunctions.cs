using System;

namespace Timesheet.Api.Utils
{
    public static class NumericFunctions
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
}
