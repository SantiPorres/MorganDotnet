using FluentValidation;

namespace Domain.Validators
{
    public class TaskValidator : AbstractValidator<Entities.Task>
    {
        public TaskValidator()
        {
            RuleFor(task => task.Title)
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty")
                .MaximumLength(80)
                .WithMessage("{PropertyName} must be shorter than {MaxLength} characters");

            RuleFor(task => task.Description)
                .MaximumLength(300)
                .WithMessage("{PropertyName} must be shorter than {MaxLength} characters");

            RuleFor(task => task.ProjectId)
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty");

            RuleFor(task => task.UserId);
        }
    }
}
