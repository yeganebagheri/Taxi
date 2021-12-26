using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Models;
using Taxi.QueryModels;

namespace Taxi.Contracts
{
    public interface ITripRepository 
    {
        Task<TripQuery> Get(int id);
        Task<IEnumerable<TripQuery>> GetAll();
        Task<Trip> Add(Trip entity, TripsNum service);
        Task<int> Delete(int id);
        Task<int> Update(Trip entity);

    }
}
