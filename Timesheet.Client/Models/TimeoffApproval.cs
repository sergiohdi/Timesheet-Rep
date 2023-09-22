using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timesheet.Client.Models
{
    public class TimeoffApproval : IValidatableObject
    {
        public int ApprovalId { get; set; }
        public int UserId { get; set; }
        public int ApprovalStatusId { get; set; }
        public int? TimeOffId { get; set; }
        public int ApprovalType { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
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

            if (Duration <= 0.10M)
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
}
