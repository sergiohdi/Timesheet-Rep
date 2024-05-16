using System.Collections.Generic;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Interfaces;

public interface IApprovalStatusBusiness
{
    IEnumerable<ApprovalStatusDto> GetApprovalStatuses();
}
