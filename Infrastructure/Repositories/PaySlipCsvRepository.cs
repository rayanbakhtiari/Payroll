using Application;
using CsvHelper;
using Domain.PaySlip;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal class PaySlipCsvRepository : IPaySlipRepository
    {
        private readonly string inputFileAddress;
        private readonly ILogger<IPaySlipRepository> logger;
        private readonly string? outputFileAddres;

        public PaySlipCsvRepository(string inputFileAddress, ILogger<IPaySlipRepository> logger)
        {
            this.inputFileAddress = inputFileAddress;
            this.logger = logger;
        }
        public PaySlipCsvRepository(string inputFileAddress, string outputFileAddres,ILogger<IPaySlipRepository> logger )
        {
            this.inputFileAddress = inputFileAddress;
            this.outputFileAddres = outputFileAddres;
            this.logger = logger;
        }
        public async Task<List<MonthlyPaySlipInput>> GetMonthlyPaySlipInputList()
        {
            if (string.IsNullOrEmpty(inputFileAddress))
                throw new ArgumentNullException("inputFileAddress");
            List<MonthlyPaySlipInput> monthlyPaySlipInputList = new();
            using(var reader = new StreamReader(this.inputFileAddress))
                using(var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                logger.LogInformation("Reading monthly pay slip input information from file {inputFileAddress} ...",inputFileAddress);
                var csvPaySlipInputs = csv.GetRecordsAsync<MonthlyPaySlipInput>();
                await foreach(var record in csvPaySlipInputs)
                {
                    monthlyPaySlipInputList.Add(record);
                }
            }
            return monthlyPaySlipInputList;
        }

        public async Task InsertMonthlyPaySlipOutputList(List<MonthlyPaySlipOutput> result)
        {
            if (string.IsNullOrEmpty(outputFileAddres))
                throw new ArgumentNullException("outputFileAddress");
            using(var writer = new StreamWriter(this.outputFileAddres))
                using(var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                logger.LogInformation("Writing monthly pay slip calculation result on file  {outputFileAddres} ...", outputFileAddres);
                await csv.WriteRecordsAsync(result);
                logger.LogInformation("{outputFileAddres} successfully created.", outputFileAddres);
            }
        }
    }
}
