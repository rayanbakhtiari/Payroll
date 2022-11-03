using Domain;
using Domain.PaySlip;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EmployeeMonthlyPaySlip
{
    internal class GetMonthPaySlipQueryHandler : IRequestHandler<GetMonthlyPaySlipsQuery, GetMonthlyPaySlipsResponse>
    {
        private readonly IPaySlipRepository paySlipRepository;

        public GetMonthPaySlipQueryHandler(IPaySlipRepository paySlipRepository)
        {
            this.paySlipRepository = paySlipRepository;
        }
        public async Task<GetMonthlyPaySlipsResponse> Handle(GetMonthlyPaySlipsQuery request, CancellationToken cancellationToken)
        {
            GetMonthlyPaySlipsResponse response = new();
            List<MonthlyPaySlipInput> monthlyPaySlipInputList = await paySlipRepository.GetMonthlyPaySlipList();

            MonthlyPaySlipInputValidator inputValidator;
            monthlyPaySlipInputList?.ForEach(monthlyPaySlipInput =>
            {
                inputValidator = new();
                var result = inputValidator.Validate(monthlyPaySlipInput);
                if (!result.IsValid)
                    throw new MonthlyPaySlipInputValidationException($"input validation error at index {monthlyPaySlipInputList.IndexOf(monthlyPaySlipInput)}", result.Errors);

                MonthlyPaySlipOutput monthlyPaySlipOutput = new();
                monthlyPaySlipOutput.PayPeriod = getPayPeriod(monthlyPaySlipInput.PayPeriod);
                monthlyPaySlipOutput.Name = monthlyPaySlipInput.GetFullName();
                monthlyPaySlipOutput.GrossIncome = monthlyPaySlipInput.GetGrossIncome();

                response.Result.Add(monthlyPaySlipOutput);
            });
            return response;
        }

        private string getPayPeriod(string month)
        {
            int monthNumber = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;
            int numberOfDays = DateTime.DaysInMonth(DateTime.Now.Year, monthNumber);
            string dayInMonth = $"01 {month} - {numberOfDays} {month}";
            return dayInMonth;
        }
    }
}
