namespace Timesheet.Shared.Utils
{
    public static class DateFunctions
    {
        public static DateTime GetPeriod(int year, int month, int period)
        {
            return period == 2 ? new DateTime(year, month, 16) : new DateTime(year, month, 1);
        }

        public static DateTime GetPeriodStartDate(int year, int month, int day)
        {
            return day > 15 ? new DateTime(year, month, 16) : new DateTime(year, month, 1);
        }

        public static DateTime GetPeriodStartDate(DateTime date)
        {
            return date.Day > 15 ? new DateTime(date.Year, date.Month, 16) : new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime GetPeriodLastDate(int year, int month, int day)
        {
            int lastDay = GetDaysInMonth(year, month);
            return day < 16 ? new DateTime(year, month, 15) : new DateTime(year, month, lastDay);
        }

        public static DateTime GetPeriodLastDate(DateTime date)
        {
            int lastDay = GetDaysInMonth(date.Year, date.Month);
            return date.Day < 16 
                ? new DateTime(date.Year, date.Month, 15) 
                : new DateTime(date.Year, date.Month, lastDay);
        }

        public static string GetDatePeriod(DateTime date)
        {
            return date.Day > 15 ? "2-2" : "1-2";
        }

        public static long GetPeriodInTicks(int year, int month, int day)
        {
            return new DateTime(year,
                                month,
                                day,
                                0,
                                0,
                                0,
                                DateTimeKind.Local).Ticks;
        }

        private static int GetDaysInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }
    }
}
