namespace Timesheet.Client.Extensions;

public static class DecimalExtensions
{
    public static string GetDecimalWithFormat(this decimal value, string format) 
    {
        return string.Format(format, value);
    }
}
