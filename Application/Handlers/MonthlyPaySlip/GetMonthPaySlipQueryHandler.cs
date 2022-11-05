using Application.TaxCalculator;
using Application.Validators;
using Domain;
using Domain.PaySlip;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.MonthlyPaySlip
{
    internal class GetMonthPaySlipQueryHandler : IRequestHandler<GetMonthlyPaySlipsQuery, GetMonthlyPaySlipsResponse>
    {
        private readonly IPaySlipRepository paySlipRepository;
        private readonly ITaxCalculator taxCalculator;
        private readonly ILogger<GetMonthPaySlipQueryHandler> logger;

        public GetMonthPaySlipQueryHandler(IPaySlipRepositoryFactory paySlipRepositoryFactory ,ITaxCalculator taxCalculator, ILogger<GetMonthPaySlipQueryHandler> logger)
        {
            this.paySlipRepository = paySlipRepositoryFactory.CreatePaySlipRepository();
            this.taxCalculator = taxCalculator;
            this.logger = logger;
        }
        public async Task<GetMonthlyPaySlipsResponse> Handle(GetMonthlyPaySlipsQuery request, CancellationToken cancellationToken)
        {
            GetMonthlyPaySlipsResponse response = new();
            List<MonthlyPaySlipInput> monthlyPaySlipInputList = await paySlipRepository.GetMonthlyPaySlipInputList();

            MonthlyPaySlipInputValidator inputValidator;
            logger.LogInformation("Calculating PayRoll ...");
            monthlyPaySlipInputList?.ForEach(paySlipInput =>
            {
                inputValidator = new();
                var result = inputValidator.Validate(paySlipInput);
                if (!result.IsValid)
                    throw new MonthlyPaySlipInputValidationException($"input validation error at index {monthlyPaySlipInputList.IndexOf(paySlipInput)}", result.Errors);

                MonthlyPaySlipOutput paySlipOutput = new();
                paySlipOutput.PayPeriod = paySlipInput.getPayPeriod();
                paySlipOutput.Name = paySlipInput.GetFullName();
                paySlipOutput.GrossIncome = paySlipInput.GetGrossIncome();
                paySlipOutput.IncomeTax = taxCalculator.GetTaxForAnnualSalary(paySlipInput.AnnualSalary);
                paySlipOutput.Super = paySlipInput.GetSuperRate(); 

                response.Result.Add(paySlipOutput);
            });

            await paySlipRepository.InsertMonthlyPaySlipOutputList(response.Result);
            return response;
        }

    }
}
