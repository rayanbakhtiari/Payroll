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
    internal class PaySlipCsvRepository : IPaySlipRepository
    {
        private readonly string inputFileAddress;
        private readonly ILogger<PaySlipCsvRepository > logger;
        private readonly string? outputFileAddres;

        public PaySlipCsvRepository(string inputFileAddress, ILoggerFactory loggerFactory)
        {
            this.inputFileAddress = inputFileAddress;
            this.logger = loggerFactory.CreateLogger<PaySlipCsvRepository>();
        }
        public PaySlipCsvRepository(string inputFileAddress, string outputFileAddres, ILoggerFactory loggerFactory)
        {
            this.inputFileAddress = inputFileAddress;
            this.outputFileAddres = outputFileAddres;
            this.logger = loggerFactory.CreateLogger<PaySlipCsvRepository>();
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

        public async Task InsertMonthlyPaySlipOutputList(List<MonthlyPaySlipOutput> result)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                Encoding = Encoding.UTF8,
            };
            if (string.IsNullOrEmpty(outputFileAddres))
                throw new ArgumentNullException("outputFileAddress");
            using (var writer = new StreamWriter(outputFileAddres))
            using (var csv = new CsvWriter(writer, csvConfig))
            {
                logger.LogInformation("Writing monthly pay slip calculation result on file  {outputFileAddres} ...", outputFileAddres);
                await csv.WriteRecordsAsync(result);
                logger.LogInformation("{outputFileAddres} successfully created.", outputFileAddres);
            }
        }
    }
}
