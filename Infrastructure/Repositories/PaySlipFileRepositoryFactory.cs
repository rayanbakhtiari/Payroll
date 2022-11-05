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

namespace Infrastructure.Repositories
{
    public class PaySlipFileRepositoryFactory : IPaySlipRepositoryFactory
    {
        private readonly ILogger<IPaySlipRepository> logger;
        public string? InputFileAddress { get; set; }
        public string? OutputFileAddress { get; set; }

        public PaySlipFileRepositoryFactory(ILogger<IPaySlipRepository> logger )
        {
            this.logger = logger;
        }

        public IPaySlipRepository CreatePaySlipRepository()
        {
            if(InputFileAddress is null)
                throw new ArgumentNullException(nameof(InputFileAddress));
            if(OutputFileAddress is null)
                return new PaySlipCsvRepository(InputFileAddress, logger);
            else
                return new PaySlipCsvRepository(InputFileAddress, OutputFileAddress , logger);
        }
    }
}
