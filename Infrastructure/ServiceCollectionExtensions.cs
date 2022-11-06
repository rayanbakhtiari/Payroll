using Application;
using Application.Handlers.MonthlyPaySlip;
using Infrastructure.Repositories.PaySlip;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
        }
        public static void AddPaySlipFileRepositoryForConsoleApp(this IServiceCollection services, string inputFileAddress, string outputFileAddress)
        {
            services.AddScoped<IPaySlipRepositoryFactory>(p => {
                ILoggerFactory? logger = p.GetService<ILoggerFactory>();
                PaySlipFileRepositoryFactory paySlipFactory = new(logger?? new NullLoggerFactory())
                {
                    InputFileAddress = inputFileAddress,
                    OutputFileAddress = outputFileAddress
                };
                return paySlipFactory;
            });
        }
    }
}
