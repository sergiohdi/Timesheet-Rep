namespace Timesheet.Client.Utils;

public static class Constants
{
    public const string base_url = "http://localhost:44368/api";
    public const string base_url_client = "http://localhost:25008/";
    public const string costCenterEndpoint = "costcenter";
    public const string activityEndpoint = "activity";
    public const string clientEndpoint = "client";
    public const string projectEndpoint = "project";
    public const string departmentEndpoint = "department";
    public const string timesheetTypeEndpoint = "timesheettype";
    public const string userEndpoint = "user";
    public const string loginEndpoint = "login";
    public const string userActivityCodeEndpoint = "useractivitycode";
    public const string ProjectHasUserEndpoint = "projecthasuser";
    public const string timesheetEndpoint = "timesheetdata";
    public const string approvalEndpoint = "approval";
    public const string substituteEndpoint = "substitute";
    public const string timesheetControlEndpoint = "timesheetcontrol";
    public const string approvalHistoryEndPoint = "approvalhistory";
    public const string responseError = "An error occurred while processing your request.";
    public const string decimalFormat = "{0:0.00}";
    // Todo: check if we still need these constants
    public const string adminRole = "36";
    public const string userRole = "37";
    public const string wtsRole = "38";
}

public static class NotificationType
{
    public const string Info = "info";
    public const string Success = "success";
    public const string Warning = "warning";
    public const string Error = "error";
}
