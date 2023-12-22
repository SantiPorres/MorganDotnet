using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class UserProjectValidator : AbstractValidator<UserProject>
    {
        public UserProjectValidator()
        {
            RuleFor(userProject => userProject.UserId)
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty")
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than {ComparisonValue}");

            RuleFor(userProject => userProject.ProjectId)
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty")
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than {ComparisonValue}");

            RuleFor(userProject => userProject.Role)
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty")
                .IsInEnum()
                .WithMessage("{PropertyValue} is not a valid {PropertyName}");
        }
    }
}
