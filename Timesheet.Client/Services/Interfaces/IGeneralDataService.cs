using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces;

public interface IGeneralDataService
{
    Task<ApiResponse<IEnumerable<General>>> GetGeneralValues(string group);
    Task<ApiResponse<IEnumerable<DateTime>>> GetWeekendDates();
}