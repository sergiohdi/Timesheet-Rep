using System.Collections.Generic;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Implementations;

public class DepartmentBusiness : IDepartmentBusiness
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IUserRepository _userRepository;

    public DepartmentBusiness(
        IDepartmentRepository departmentRepository,
        IUserRepository userRepository
        )
    {
        _departmentRepository = departmentRepository;
        _userRepository = userRepository;
    }

    public IEnumerable<DepartmentDto> GetDepartments(bool? DisabledSetting) => _departmentRepository.GetDepartments(DisabledSetting);

    public DepartmentDto GetDepartmentById(int Id) => _departmentRepository.GetDepartmentById(Id);

    public bool CreateDepartment(DepartmentDto department) => _departmentRepository.CreateDepartment(department);

    public bool UpdateDepartment(DepartmentDto department) => _departmentRepository.UpdateDepartment(department);

    // Todo: Check if this code is usefull
    //public bool UpdateDepartmentState(int departmentId)
    //{
    //    DepartmentDto department = _departmentRepository.GetDepartmentById(departmentId);
    //    department.Disabled = !department.Disabled;

    //    return _departmentRepository.UpdateDepartmentState(department);
    //}

    public bool DeleteDepartment(int Id)
    {
        // Validate if the department exists
        DepartmentDto department = _departmentRepository.GetDepartmentById(Id);
        if (department is null)
        {
            return false;
        }

        // Validate if the department is used in any User
        bool isUsedDepartmentInUser = _userRepository.ValidateUsersByDepartmentId(Id);

        if (isUsedDepartmentInUser)
        {
            return false;
        }

        bool result;
        try
        {
            result = _departmentRepository.DeleteDepartment(department);
        }

        catch (System.Exception ex)
        {
            if (!ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE"))
            {
                throw;
            }

            result = false;
        }

        return result;
    }
}
