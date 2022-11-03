using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace Application.UnitTests
{
    public class SetupFixture : IDisposable
    {
        private IServiceCollection? _services;
        public IMediator? Mediator { get; private set; }

        public Mock<IPaySlipRepository> PaySlipRepositoryMock { get; private set; }


        public SetupFixture()
        {
            PaySlipRepositoryMock = new();
            _services = new ServiceCollection();
            _services.InjectApplicationHandlers();
            _services.AddSingleton<IPaySlipRepository>(PaySlipRepositoryMock.Object);
            var provider = _services.BuildServiceProvider();
            Mediator = provider.GetService<IMediator>();    

        }
        public void Dispose()
        {
            _services?.Clear();
            _services = null;
            Mediator = null;
        }
    }
}
