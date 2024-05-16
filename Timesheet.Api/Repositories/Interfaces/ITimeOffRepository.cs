using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces;

public interface ITimeOffRepository
{
    IEnumerable<TimeOffDto> GetTimeOffList();
    TimeOffDto GetTimeOffById(int timeOffId);
}
