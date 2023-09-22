namespace Timesheet.Client.Utils
{
    public enum ResponseStatus
    {
        Success,
        Error
    }

    public enum RecordType
    {
        Regular = 1,
        TimeOff = 2
    }

    public enum TimesheetItemAction
    {
        Add = 1,
        Update = 2,
        Delete = 3,
    }

    public enum Property
    {
        Client,
        Project,
        Activity,
        PONumber,
        Billing,
        Location
    }
}
