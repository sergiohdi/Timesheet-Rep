using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations
{
    public class SubstituteBusiness : ISubstituteBusiness
    {
        private readonly ISubstituteRepository _substituteRepository;

        public SubstituteBusiness(ISubstituteRepository substituteRepository)
        {
            _substituteRepository = substituteRepository;
        }

        public IEnumerable<SubstituteDto> GetSubstitutesByUserId(int userId)
        {
            return _substituteRepository.GetUsersSubstituteByUserId(userId);
        }

        public bool UpdateSubstitutes(List<SubstituteDto> substitutes, int userId)
        {
            _substituteRepository.RemoveSubstitutes(userId);

            return _substituteRepository.InsertSubstitutes(substitutes);
        }
    }
}
