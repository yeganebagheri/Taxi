using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Models
{
    public class Car
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public int CarTage { get; set; }
        public CarType CarType { get; set; }
    }
}
