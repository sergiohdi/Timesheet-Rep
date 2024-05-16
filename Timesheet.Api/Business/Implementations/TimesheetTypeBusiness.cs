using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations;

public class TimesheetTypeBusiness : ITimesheetTypeBusiness
{
    private readonly ITimesheetTypeRepository _timesheetTypeRepository;

    public TimesheetTypeBusiness(ITimesheetTypeRepository timesheetTypeRepository) => _timesheetTypeRepository = timesheetTypeRepository;

    public IEnumerable<TimesheetTypeDto> GetTimesheetTypes() => _timesheetTypeRepository.GetTimesheetTypes();
}
