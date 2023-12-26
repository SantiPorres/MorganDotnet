using FluentValidation.Results;

namespace Domain.CustomExceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("There have been one or multiple validation errors")
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; set; }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}
