﻿using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Repositories.Interfaces;

public interface IParentDepartmentRepository
{
    IEnumerable<ParentDepartmentDto> GetParentDepartments();
}
