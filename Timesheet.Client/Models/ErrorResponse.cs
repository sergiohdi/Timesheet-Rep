namespace Timesheet.Client.Models;

public class ErrorResponse
{
    public dynamic Errors { get; set; }
    public int Status { get; set; }
    public string Title { get; set; }
}
