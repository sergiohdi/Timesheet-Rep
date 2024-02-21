namespace Timesheet.Api.Utils
{
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

    public enum BillableOptions
    {
        BillableAndNoBillable = 9,
        NonBillable = 10,
        Billable = 11,
    }
}
