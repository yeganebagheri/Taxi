using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Contracts
{
    public interface ITripServiceRepository
    {
        public Task<int> Get(int NationalCode);
        public Task Update(int id);
    }
}
