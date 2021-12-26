using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Dto;
using Taxi.Models;
using Taxi.QueryModels;

namespace Taxi.Contracts
{
    public interface IDriverRepository
    {
        Task<DriverQuery> Get(int id);
        Task<IEnumerable<DriverDto>> GetAll();
        Task<Driver> Add(Driver entity);
        Task<int> Delete(int id);
        Task<int> Update(Driver entity);
    }
}
