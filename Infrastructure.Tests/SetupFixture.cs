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
        private IServiceCollection? _services;


        public SetupFixture()
        {
            SetCultureInfoToEnUS();
            _services = new ServiceCollection();
            _services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
            _services.BuildServiceProvider();

        }
        private void SetCultureInfoToEnUS()
        {
            var cultureInfo = new CultureInfo("en-US");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
        public IPaySlipRepositoryFactory AddPaySlipFileRepositoryFactoryWithFileAddress(string inputFileAddress, string outputFileAddress)
        {
            if (_services is null)
                _services = new ServiceCollection();
            _services.AddPaySlipFileRepositoryForConsoleApp(inputFileAddress, outputFileAddress);
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
