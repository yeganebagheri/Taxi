using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Models;

namespace Taxi.Contracts
{
    public interface IPassengerRepository
    {
        Task<Passenger> Get(int id);
        Task<IEnumerable<Passenger>> GetAll();
        Task<Passenger> Add(Passenger entity);
        Task<int> Delete(int id);
        Task<int> Update(Passenger entity);
    }
}
