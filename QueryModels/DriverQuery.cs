using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.QueryModels
{
    public class DriverQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NationalCode { get; set; }
        public string Gender { get; set; }
        public int Birthday { get; set; }
        public int CarTag { get; set; }
        public string CarName { get; set; }
        public bool Passengerable { get; set; }
    }
}
