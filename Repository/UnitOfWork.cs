using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Contracts;

namespace Taxi.Repository
{   
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ITripRepository tripRepository  , IDriverRepository driverRepository, IPassengerRepository passenger)
        {
            Trip = tripRepository;
            Driver = driverRepository;
            Passenger = passenger;
        }
        
        public ITripRepository Trip { get; }

        public IDriverRepository Driver { get; }

        public IPassengerRepository Passenger { get; }
    }
}
