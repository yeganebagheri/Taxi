using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Contracts;

namespace Taxi.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ITripRepository, TripRepository>();
            services.AddTransient<IDriverRepository, DriverRepository>();
            services.AddTransient<IPassengerRepository, PassengerRepository>();
            services.AddTransient<IRabbitMqRepository, RabbitMqRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
