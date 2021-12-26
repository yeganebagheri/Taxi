using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NationalCode { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public int CarTage { get; set; }
        public string CarName { get; set; }
        public bool Passengerable { get; set; }
        public Car Car { get; set; }
        public CarType CarType { get; set; }
    }
}
