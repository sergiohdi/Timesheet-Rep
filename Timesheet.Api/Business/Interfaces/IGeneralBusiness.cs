using System;
using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces;

public interface IGeneralBusiness
{
    IEnumerable<GeneralDto> GetGeneralRecords(string group);
    IEnumerable<DateTime> GetWeekendDates();
}