using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.QueryModels
{
    public class TripQuery
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public int Cost { get; set; }
        public bool EndTrip { get; set; }
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public int DriverNationalCode { get; set; }
        public int PassengerId { get; set; }
        public string PassengerName { get; set; }
        public int PassengerNationalCode { get; set; }
    }
}
