using Domain.PaySlip;
using FluentValidation;
using System.Globalization;

namespace Application.Validators
{
    public class MonthlyPaySlipInputValidator : AbstractValidator<MonthlyPaySlipInput>
    {

        public MonthlyPaySlipInputValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.AnnualSalary).GreaterThan(0);
            RuleFor(p => p.SuperRate).NotEmpty().Must(p => {
                int super;
                bool isParsable =  Int32.TryParse(p.Replace("%",""), out super);
                if (!isParsable)
                    return false;
                return super >= 0 && super <= 50;
            });
            RuleFor(p => p.PayPeriod).NotEmpty().Must(p =>
            {
                DateTime dateTime;
                return DateTime.TryParseExact(p, "MMMM", CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTime);
            }).WithMessage(p => $"{p.PayPeriod} is not valid month name.");
        }
    }
}