using Application.DTOs.AssignmentDTOs;
using FluentValidation;

namespace Application.Validators.AssignmentValidators
{
    public class CreateAssignmentDTOValidator : AbstractValidator<CreateAssignmentDTO>
    {
        public CreateAssignmentDTOValidator()
        {
            RuleFor(assignment => assignment.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty")
                .MaximumLength(80)
                .WithMessage("{PropertyName} must be shorter than {MaxLength} characters");

            RuleFor(assignment => assignment.Description)
                .MaximumLength(300)
                .WithMessage("{PropertyName} must be shorter than {MaxLength} characters");
        }
    }
}
