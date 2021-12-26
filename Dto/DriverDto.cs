using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Dto
{
    public class DriverDto
    {
        public string Name { get; set; }
        public int NationalCode { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }     
        public int CarTage { get; set; }
        public string CarName { get; set; }
        public bool Passengerable { get; set; }
    }


    public class DriverDtoValidator : AbstractValidator<DriverDto>
    {
        public DriverDtoValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.NationalCode).NotNull();
            RuleFor(x => x.Gender).Length(6, 46);
            RuleFor(x => x.CarName).NotNull();
            RuleFor(x => x.Birthday)
                .InclusiveBetween(
                    new DateTime(1971, 1, 1), DateTime.Now.AddYears(-10));
        }
    }
}
