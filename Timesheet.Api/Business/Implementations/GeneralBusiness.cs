using System;
using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Api.Utils;

namespace Timesheet.Api.Business.Implementations
{
    public class GeneralBusiness : IGeneralBusiness
    {
        private readonly IGeneralRepository _generalRepository;

        public GeneralBusiness(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public IEnumerable<GeneralDto> GetGeneralRecords(string group)
        {
            return _generalRepository.GetGeneralRecords(group);
        }

        public IEnumerable<DateTime> GetWeekendDates()
        {
            return ApiDateFunctions.GetWeekendDates();
        }
    }
}
