using Application.Filters;
using FluentValidation;

namespace Application.Validators
{
    public class PaginationQueryParametersValidator : AbstractValidator<PaginationQueryParameters>
    {
        public PaginationQueryParametersValidator()
        {
            RuleFor(pqp => pqp.PageSize)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than {ComparisonValue}");

            RuleFor(pqp => pqp.PageNumber)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than {ComparisonValue}");
        }
    }
}
