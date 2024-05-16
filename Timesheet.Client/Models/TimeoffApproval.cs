using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timesheet.Client.Models;

public class TimeoffApproval :  Approval, IValidatableObject
{
    public TimeoffApproval()
    {
        StartDate = DateTime.Now;
        EndDate = DateTime.Now;
    }

    public int TimeOffId { get; set; }
    public decimal? Duration { get; set; }
    public string Comments { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errorsResult = new List<ValidationResult>();

        if (TimeOffId == 0) 
        {
            errorsResult.Add(new ValidationResult(
                errorMessage: "Timeoff is required",
                memberNames: new[] { "TimeoffTypeId" }
             ));
        }

        if (Duration is null || Duration <= 0.10M)
        {
            errorsResult.Add(new ValidationResult(
                errorMessage: "Duration is required",
                memberNames: new[] { "Duration" }
             ));
        }

        if (EndDate.Date < StartDate.Date)
        {
             errorsResult.Add(new ValidationResult(
                errorMessage: "EndDate must be greater than StartDate",
                memberNames: new[] { "EndDate" }
             ));
        }

        return errorsResult;
    }
}
