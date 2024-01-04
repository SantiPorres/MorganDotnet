using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class AssignmentValidator : AbstractValidator<Assignment>
    {
        public AssignmentValidator()
        {
            RuleFor(a => a.Title)
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty")
                .MaximumLength(80)
                .WithMessage("{PropertyName} must be shorter than {MaxLength} characters");

            RuleFor(a => a.Description)
                .MaximumLength(300)
                .WithMessage("{PropertyName} must be shorter than {MaxLength} characters");

            RuleFor(a => a.ProjectId)
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty");

            RuleFor(a => a.UserId);
        }
    }
}
