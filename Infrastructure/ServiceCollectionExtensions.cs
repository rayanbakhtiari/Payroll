using Application;
using Application.Handlers.MonthlyPaySlip;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
                var logger = p.GetService<ILogger<IPaySlipRepository>>();
                var paySlipFactory = new PaySlipFileRepositoryFactory(logger);
                paySlipFactory.InputFileAddress = inputFileAddress;
                paySlipFactory.OutputFileAddress = outputFileAddress;
                return paySlipFactory;
            });
        }
    }
}
