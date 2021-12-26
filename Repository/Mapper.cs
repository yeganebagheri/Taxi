using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Dto;
using Taxi.Models;

namespace Taxi.Repository
{
    public class Mapper : Profile
    {

        public Mapper()
        {
            CreateMap<DriverDto, Driver>();
            CreateMap<PassengerDto, Passenger>();
            CreateMap<TripDto, Trip>();


        }
    }
    
}
