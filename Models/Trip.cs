using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public int Cost { get; set; }
        public bool EndTrip { get; set; } = false;
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public int PassengerId { get; set; }
        public int PassengerNationalCode { get; set; }
        public Passenger Passenger { get; set; }
    }
}
