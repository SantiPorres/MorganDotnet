using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class UserProjectValidator : AbstractValidator<UserProject>
    {
        public UserProjectValidator()
        {
            RuleFor(userProject => userProject.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty");

            RuleFor(userProject => userProject.ProjectId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty");

            RuleFor(userProject => userProject.Role)
                .IsInEnum()
                .WithMessage("{PropertyValue} is not a valid {PropertyName}");
        }
    }
}
