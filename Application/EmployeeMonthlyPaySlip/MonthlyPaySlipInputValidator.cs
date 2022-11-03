using Domain.PaySlip;
using FluentValidation;
using System.Globalization;

namespace Application.EmployeeMonthlyPaySlip
{
    public class MonthlyPaySlipInputValidator: AbstractValidator<MonthlyPaySlipInput>
    {
        public MonthlyPaySlipInputValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.AnnualSalary).GreaterThan(0);
            RuleFor(p => p.SuperRate).GreaterThanOrEqualTo(0).LessThanOrEqualTo(50);
            RuleFor(p => p.PayPeriod).NotEmpty().Must(p =>
            {
                DateTime dateTime;
                return DateTime.TryParseExact(p, "MMMM", CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTime);
            }).WithMessage(p => $"{p.PayPeriod} is not valid month name.");
        }
    }
}