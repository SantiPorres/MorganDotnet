using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Username)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty")
                .MaximumLength(50)
                .WithMessage("{PropertyName} must be shorter than {MaxLength} characters");

            RuleFor(user => user.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty")
                .EmailAddress()
                .WithMessage("{PropertyName} must be a valid email address")
                .MaximumLength(130)
                .WithMessage("{PropertyName} must be shorter than {MaxLength} characters");

            RuleFor(user => user.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty")
                .MaximumLength(100)
                .WithMessage("{PropertyName} must be shorter than {MaxLength} characters");
        }
    }
}
