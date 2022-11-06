using Application;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.PaySlip;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text;

namespace Infrastructure.Repositories.PaySlip
{
    internal class PaySlipCsvRepositoryWithInputStream : PaySlipCsvRepositoryBase,IPaySlipRepository 
    {
        private readonly Stream inputStream;

        public PaySlipCsvRepositoryWithInputStream(Stream inputStream, string outputFileAddress, ILoggerFactory loggerFactory): base(outputFileAddress,loggerFactory)
        {
            this.inputStream = inputStream;
        }

        public async Task<List<MonthlyPaySlipInput>> GetMonthlyPaySlipInputList()
        {
            List<MonthlyPaySlipInput> monthlyPaySlipInputList = new();
            using (var reader = new StreamReader(inputStream))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                csv.Context.RegisterClassMap<MonthlyPaySlipInputMapper>();
                logger.LogInformation("Reading monthly pay slip input information from stream} ...");
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