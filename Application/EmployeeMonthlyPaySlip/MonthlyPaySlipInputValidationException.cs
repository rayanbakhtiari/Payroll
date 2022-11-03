using FluentValidation.Results;

namespace Application.EmployeeMonthlyPaySlip
{
    public class MonthlyPaySlipInputValidationException: Exception
    {
        public MonthlyPaySlipInputValidationException(string message, List<ValidationFailure> errors): base(message)
        {
            this.Errors = errors;
        }
        public List<ValidationFailure> Errors { get; set; } = new();
    }
}