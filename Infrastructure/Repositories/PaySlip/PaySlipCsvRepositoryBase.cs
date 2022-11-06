using Application;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.PaySlip;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text;

namespace Infrastructure.Repositories.PaySlip
{
    internal class PaySlipCsvRepositoryBase
    {
        protected readonly string? outputFileAddress;
        protected readonly ILogger<IPaySlipRepository> logger;

        public PaySlipCsvRepositoryBase(string? outputFileAddress, ILoggerFactory loggerFactory)
        {
            this.outputFileAddress = outputFileAddress;
            this.logger = loggerFactory.CreateLogger<IPaySlipRepository>();
        }

        public async Task InsertMonthlyPaySlipOutputList(List<MonthlyPaySlipOutput> result)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                Encoding = Encoding.UTF8,
            };
            if (string.IsNullOrEmpty(outputFileAddress))
                throw new ArgumentNullException("outputFileAddress");
            using (var writer = new StreamWriter(outputFileAddress))
            using (var csv = new CsvWriter(writer, csvConfig))
            {
                logger.LogInformation("Writing monthly pay slip calculation result on file  {outputFileAddres} ...", outputFileAddress);
                await csv.WriteRecordsAsync(result);
                logger.LogInformation("{outputFileAddres} successfully created.", outputFileAddress);
            }
        }
    }
}