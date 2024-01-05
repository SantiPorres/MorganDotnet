using Application.DTOs.AccountDTOs;
using FluentValidation;

namespace Application.Validators.AccountValidators
{
    public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
    {
        public LoginUserDTOValidator()
        {
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
