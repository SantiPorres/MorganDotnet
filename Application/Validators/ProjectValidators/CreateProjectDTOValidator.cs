using Application.DTOs.ProjectDTOs;
using FluentValidation;

namespace Application.Validators.ProjectValidators
{
    public class CreateProjectDTOValidator : AbstractValidator<CreateProjectDTO>
    {
        public CreateProjectDTOValidator()
        {
            RuleFor(project => project.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty")
                .MaximumLength(80)
                .WithMessage("{PropertyName} must be shorter than {MaxLength} characters");

            RuleFor(project => project.Description)
                .MaximumLength(300)
                .WithMessage("{PropertyName} must be shorter than {MaxLength} characters");
        }
    }
}
