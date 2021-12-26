using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NationalCode { get; set; }
        public string Gender { get; set; }
    }
}
