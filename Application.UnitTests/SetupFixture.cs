using Application.Handlers.MonthlyPaySlip;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.UnitTests
{
    public class SetupFixture : IDisposable
    {
        private IServiceCollection? _services;
        public IMediator? Mediator { get; private set; }

        public Mock<IPaySlipRepository> PaySlipRepositoryMock { get; private set; }
        private Mock<IPaySlipRepositoryFactory> paySlipRepositoryFactoryMock = new();


        public SetupFixture()
        {
            SetCultureInfoToEnUS();
            PaySlipRepositoryMock = new();
            paySlipRepositoryFactoryMock.Setup(p => p.CreatePaySlipRepository()).Returns(PaySlipRepositoryMock.Object);
            _services = new ServiceCollection();
            _services.AddApplicationServices();
            _services.AddSingleton<IPaySlipRepositoryFactory>(paySlipRepositoryFactoryMock.Object);
            var provider = _services.BuildServiceProvider();
            Mediator = provider.GetService<IMediator>();

        }
        private void SetCultureInfoToEnUS()
        {
            var cultureInfo = new CultureInfo("en-US");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }

        public void Dispose()
        {
            _services?.Clear();
            _services = null;
            Mediator = null;
        }
    }
}
