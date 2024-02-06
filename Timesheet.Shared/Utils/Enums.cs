namespace Timesheet.Shared.Utils
{
    public enum ApprovalType
    {
        RegularTime = 1,
        Timeoff = 2,
    }

    public enum ApprovalStatusOption
    {
        NotSubmitted = 0,
        Waiting = 1,
        Approved = 2,
        Rejected = 3,
        SupervisorApproval = 4,
    }

    public enum UserTimesheetTemplate
    {
        DailyShift = 1,
        EightHoursShift = 2,
        DynamicHoursShift = 3,
        None = 4
    }

    public enum DayValuesError
    {
        NoError = 0,
        OneDayShiftError = 1,
        EightHoursShiftError = 2,
    }
}
