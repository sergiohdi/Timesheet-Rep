using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations;

public class TimeOffBusiness : ITimeOffBusiness
{
    private readonly ITimeOffRepository _timeOffRepository;

    public TimeOffBusiness(ITimeOffRepository timeOffRepository) => _timeOffRepository = timeOffRepository;

    public IEnumerable<TimeOffDto> GetTimeOffList() => _timeOffRepository.GetTimeOffList();
}
