using FluentValidation;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Utils;

namespace Timesheet.Api.Validators
{
    // Superclass that has all validator classes for all dtos
    public class ActivityValidator : AbstractValidator<ActivityDto>
    {
        public ActivityValidator()
        {
            RuleFor(x => x.ActivityName)
                .Must(v => !string.IsNullOrEmpty(v))
                .WithMessage(Constants.FIELD_REQUIRED);
            RuleFor(x => x.ActivityCode)
                .Must(v => !string.IsNullOrEmpty(v))
                .WithMessage(Constants.FIELD_REQUIRED);
        }
    }

    public class ClientValidator : AbstractValidator<ClientDto>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Name)
                .Must(v => !string.IsNullOrEmpty(v))
                .WithMessage(Constants.FIELD_REQUIRED);
            RuleFor(x => x.Code)
                .Must(v => !string.IsNullOrEmpty(v))
                .WithMessage(Constants.FIELD_REQUIRED);
            RuleFor(x => x.Name)
                .MinimumLength(5)
                .WithMessage("Client name cannot shorter than 5 characters");
        }
    }

    public class CostCenterValidator : AbstractValidator<CostCenterDto>
    {
        public CostCenterValidator()
        {
            RuleFor(x => x.Costcentername)
                .Must(v => !string.IsNullOrEmpty(v))
                .WithMessage(Constants.FIELD_REQUIRED);
        }
    }

    public class DepartmentValidator : AbstractValidator<DepartmentDto>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Name)
                .Must(v => !string.IsNullOrEmpty(v))
                .WithMessage(Constants.FIELD_REQUIRED);
            //RuleFor(x => x.Code)
            //    .Must(v => !string.IsNullOrEmpty(v))
            //    .WithMessage(Constants.FIELD_REQUIRED);
            //RuleFor(x => x.CostCenterGroup)
            //    .Must(v => !string.IsNullOrEmpty(v))
            //    .WithMessage(Constants.FIELD_REQUIRED);
        }
    }

    public class ProjectValidator : AbstractValidator<ProjectDto>
    {
        public ProjectValidator()
        {
            RuleFor(x => x.Name)
                .Must(v => !string.IsNullOrEmpty(v))
                .WithMessage(Constants.FIELD_REQUIRED);
            RuleFor(x => x.ProjectCode)
                .Must(v => !string.IsNullOrEmpty(v))
                .WithMessage(Constants.FIELD_REQUIRED);
            RuleFor(x => x.ClientId)
              .NotNull().WithMessage("{PropertyName} cannot be null")
              .GreaterThan(0);
        }
    }

    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            //RuleFor(x => x.FirstName)
            //    .Must(v => !string.IsNullOrEmpty(v))
            //    .WithMessage(Constants.FIELD_REQUIRED);
            //RuleFor(x => x.LastName)
            //    .Must(v => !string.IsNullOrEmpty(v))
            //    .WithMessage(Constants.FIELD_REQUIRED);
            //RuleFor(x => x.LoginName)
            //    .Must(v => !string.IsNullOrEmpty(v))
            //    .WithMessage(Constants.FIELD_REQUIRED);
            //RuleFor(x => x.Email)
            //    .Must(v => !string.IsNullOrEmpty(v))
            //    .WithMessage(Constants.FIELD_REQUIRED);
            //RuleFor(x => x.SupervisorId)
            //    .NotNull().WithMessage("{PropertyName} cannot be null")
            //    .GreaterThan(0);
        }
    }
}
