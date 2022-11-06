using Application;
using Application.Handlers.MonthlyPaySlip;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.PaySlip
{
    public class PaySlipFileRepositoryFactory : IPaySlipRepositoryFactory
    {
        private readonly ILoggerFactory loggerFactory;
        public string? InputFileAddress { get; set; }
        public string? OutputFileAddress { get; set; }

        public PaySlipFileRepositoryFactory(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        public IPaySlipRepository CreatePaySlipRepository()
        {
            if (InputFileAddress is null)
                throw new ArgumentNullException(nameof(InputFileAddress));
            if (OutputFileAddress is null)
                return new PaySlipCsvRepository(InputFileAddress, loggerFactory);
            else
                return new PaySlipCsvRepository(InputFileAddress, OutputFileAddress, loggerFactory);
        }
    }
}
