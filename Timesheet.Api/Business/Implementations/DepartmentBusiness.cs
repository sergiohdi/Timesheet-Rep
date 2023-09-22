using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.EF_Implementations;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Business.Interfaces
{
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

        public IEnumerable<DepartmentDto> GetDepartments(bool? DisabledSetting)
        {
            return _departmentRepository.GetDepartments(DisabledSetting);
        }

        public DepartmentDto GetDepartmentById(int Id)
        {
            return _departmentRepository.GetDepartmentById(Id);
        }

        public bool CreateDepartment(DepartmentDto department)
        {
            return _departmentRepository.CreateDepartment(department);
        }

        public bool UpdateDepartment(DepartmentDto department)
        {
            return _departmentRepository.UpdateDepartment(department);
        }

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

            return _departmentRepository.DeleteDepartment(department);
        }
    }
}
