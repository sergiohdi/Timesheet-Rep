using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.EF_Implementations;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Interfaces
{
    public class ParentDepartmentBusiness : IParentDepartmentBusiness
    {
        private readonly IParentDepartmentRepository _parentdepartmentRepository;

        public ParentDepartmentBusiness(
            IParentDepartmentRepository parentdepartmentRepository
            )
        {
            _parentdepartmentRepository = parentdepartmentRepository;

        }

        public IEnumerable<ParentDepartmentDto> GetParentDepartments()
        {
            return _parentdepartmentRepository.GetParentDepartments();
        }

    }
}
