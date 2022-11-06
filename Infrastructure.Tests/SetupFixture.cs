using Application.Handlers.MonthlyPaySlip;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Tests
{
    public class SetupFixture : IDisposable
    {
        private IServiceCollection? _services = new ServiceCollection();


        public SetupFixture()
        {
            SetCultureInfoToEnUS();
            InitializeServices();
            _services.BuildServiceProvider();

        }

        private void InitializeServices()
        {
            _services = new ServiceCollection();
            _services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        }

        private void SetCultureInfoToEnUS()
        {
            var cultureInfo = new CultureInfo("en-US");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
        public IPaySlipRepositoryFactory AddPaySlipFileRepositoryFactoryWithFileAddress(string inputFileAddress, string outputFileAddress)
        {
            InitializeServices();
            _services.AddPaySlipFileRepositoryWithInputOutputFileAddress(inputFileAddress, outputFileAddress);
            var builder = _services.BuildServiceProvider();
            var factory = builder.GetService<IPaySlipRepositoryFactory>();
            if(factory is null)
            {
                throw new InvalidOperationException("IPaySlipRepositoryFactory");
            }
            return factory;
        }
        public IPaySlipRepositoryFactory AddPaySlipFileRepositoryFactoryWithInputStream(Stream inputStream, string outputFileAddress)
        {
            InitializeServices();
            _services.AddPaySlipFileRepositoryWithInputStream(inputStream);
            var builder = _services.BuildServiceProvider();
            var factory = builder.GetService<IPaySlipRepositoryFactory>();
            if(factory is null)
            {
                throw new InvalidOperationException("IPaySlipRepositoryFactory");
            }
            return factory;
        }

        public void Dispose()
        {
            _services?.Clear();
            _services = null;
        }
    }
}
