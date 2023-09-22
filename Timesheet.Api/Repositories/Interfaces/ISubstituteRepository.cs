﻿using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces
{
    public interface ISubstituteRepository
    {
        IEnumerable<SubstituteDto> GetUsersSubstituteByUserId(int userId);
        void RemoveSubstitutes(int userId);
        bool InsertSubstitutes(List<SubstituteDto> substitutes);
    }
}
