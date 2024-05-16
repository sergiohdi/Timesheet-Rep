using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces;

public interface ISubstituteBusiness
{
    IEnumerable<SubstituteDto> GetSubstitutesByUserId(int userId);
    IEnumerable<KeyValuePair<int, string>> GetSubstitutesByEmp(int userId);
    bool UpdateSubstitutes(List<SubstituteDto> substitutes, int userId);
}
