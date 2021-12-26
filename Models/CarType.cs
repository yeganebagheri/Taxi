using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Models
{
    public class CarType
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; }
        public bool Passengerable { get; set; }
    }
}
