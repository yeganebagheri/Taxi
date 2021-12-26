using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Models;

namespace Taxi.Contracts
{
    public interface IRabbitMqRepository
    {
        void Producer(Trip trip);
    }
}
