using System.Collections.Generic;

namespace Timesheet.Client.Services.Interfaces;

public interface ITimesheetValidationsService
{
    List<string> ValidateTimesheet();
}