using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Runtime.CompilerServices;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.InjectApplicationHandlers();
        }
        public static void InjectApplicationHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions));
        }
    }
}
