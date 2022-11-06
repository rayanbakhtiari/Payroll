using FluentValidation.Results;

namespace Application.Validators
{
    public class MonthlyPaySlipInputValidationException : Exception
    {
        public MonthlyPaySlipInputValidationException(string message, List<ValidationFailure> errors) : base(message)
        {
            Errors = errors;
        }
        public List<ValidationFailure> Errors { get; set; } = new();
    }
}