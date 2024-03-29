﻿using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class AssignmentValidator : AbstractValidator<Assignment>
    {
        public AssignmentValidator()
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

            RuleFor(assignment => assignment.ProjectId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must not be empty");

            RuleFor(assignment => assignment.UserId);
        }
    }
}
