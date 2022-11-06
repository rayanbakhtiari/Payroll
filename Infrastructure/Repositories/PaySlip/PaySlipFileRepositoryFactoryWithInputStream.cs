using Application;
using Application.Handlers.MonthlyPaySlip;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.PaySlip
{
    public class PaySlipFileRepositoryFactoryWithInputStream : IPaySlipRepositoryFactory
    {
        private readonly ILoggerFactory loggerFactory;

        public Stream InputStream { get; private set; }
        public string OutputFileAddress { get; private set; }
        public PaySlipFileRepositoryFactoryWithInputStream(Stream inputStream, ILoggerFactory loggerFactory)
        {
            InputStream = inputStream;
            this.loggerFactory = loggerFactory;
            OutputFileAddress = $@"payslip-{Guid.NewGuid()}.csv";
        }

        public IPaySlipRepository CreatePaySlipRepository()
        {
            return new PaySlipCsvRepositoryWithInputStream(InputStream, OutputFileAddress,loggerFactory);
        }
    }
}
