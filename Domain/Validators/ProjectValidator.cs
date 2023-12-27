using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class ProjectValidator : AbstractValidator<Project>
    {
        public ProjectValidator()
        {
            RuleFor(project =>  project.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty")
                .MaximumLength(80)
                .WithMessage("{PropertyName} must be shorter than {MaxLength} characters");

            RuleFor(project => project.Description)
                .MaximumLength(300)
                .WithMessage("{PropertyName} must be shorter than {MaxLength} characters");

            RuleFor(project => project.OwnerId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Project must have an owner")
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than {ComparisonValue}");
        }
    }
}
