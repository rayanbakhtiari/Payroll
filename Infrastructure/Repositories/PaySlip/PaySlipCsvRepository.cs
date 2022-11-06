using Application;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.PaySlip;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.PaySlip
{
    internal class PaySlipCsvRepository : PaySlipCsvRepositoryBase, IPaySlipRepository
    {
        private readonly string inputFileAddress;

        public PaySlipCsvRepository(string inputFileAddress, ILoggerFactory loggerFactory): base(string.Empty, loggerFactory)
        {
            this.inputFileAddress = inputFileAddress;
        }
        public PaySlipCsvRepository(string inputFileAddress, string outputFileAddress, ILoggerFactory loggerFactory): base(outputFileAddress, loggerFactory)
        {
            this.inputFileAddress = inputFileAddress;
        }
        public async Task<List<MonthlyPaySlipInput>> GetMonthlyPaySlipInputList()
        {
            if (string.IsNullOrEmpty(inputFileAddress))
                throw new ArgumentNullException("inputFileAddress");
            List<MonthlyPaySlipInput> monthlyPaySlipInputList = new();
            using (var reader = new StreamReader(inputFileAddress))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                csv.Context.RegisterClassMap<MonthlyPaySlipInputMapper>();
                logger.LogInformation("Reading monthly pay slip input information from file {inputFileAddress} ...", inputFileAddress);
                var csvPaySlipInputs = csv.GetRecordsAsync<MonthlyPaySlipInput>();
                await foreach (var record in csvPaySlipInputs)
                {
                    monthlyPaySlipInputList.Add(record);
                }
            }
            return monthlyPaySlipInputList;
        }
    }
}
