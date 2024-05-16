using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations;

public class ParentDepartmentBusiness : IParentDepartmentBusiness
{
    private readonly IParentDepartmentRepository _parentdepartmentRepository;

    public ParentDepartmentBusiness(IParentDepartmentRepository parentdepartmentRepository) => _parentdepartmentRepository = parentdepartmentRepository;

    public IEnumerable<ParentDepartmentDto> GetParentDepartments() => _parentdepartmentRepository.GetParentDepartments();

}
