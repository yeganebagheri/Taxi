using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Contracts
{
    public interface IUnitOfWork
    {
        ITripRepository Trip { get; }
        IDriverRepository Driver { get; }
        IPassengerRepository Passenger { get; }
    }
}
