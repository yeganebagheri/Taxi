using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Dto
{
    public class TripDto
    {
        public string City { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public int DriverId { get; set; }
        public int PassengerId { get; set; }
        public int PassengerNationalCode { get; set; }
        public int TripsNumber { get; set; }
        public int TemperatureC { get; set; }
    }

    public class TripDtoValidator : AbstractValidator<TripDto>
    {
        public TripDtoValidator()
        {
            RuleFor(x => x.City).NotNull();
            RuleFor(x => x.Source).NotNull();
            RuleFor(x => x.Destination).NotNull();
            RuleFor(x => x.DriverId).NotNull();
            RuleFor(x => x.PassengerId).NotNull();
            RuleFor(x => x.PassengerNationalCode).NotNull();

        }
    }
}
