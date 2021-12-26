using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Dto
{
    public class PassengerDto
    {
        public string Name { get; set; }
        public int NationalCode { get; set; }
        public string Gender { get; set; }
    }


    public class PassengerDtoValidator : AbstractValidator<PassengerDto>
    {
        public PassengerDtoValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.NationalCode).NotNull();
            RuleFor(x => x.Gender).Length(6, 46);
           
        }
    }
}
