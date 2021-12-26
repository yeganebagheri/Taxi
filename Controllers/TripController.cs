using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Contracts;
using Taxi.Dto;
using Taxi.Models;

namespace Taxi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IRabbitMqRepository _Rabbit;
        private readonly ITripRepository _tripRepo;

        public TripController( IRabbitMqRepository rabbitMq, IMapper mapper, ITripRepository tripRepo)
        {
            _tripRepo = tripRepo;
            _mapper = mapper;
            _Rabbit = rabbitMq;
        }


        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            try
            {
                var currentUser = HttpContext.User;
                var trips = await _tripRepo.GetAll();
                return Ok(trips);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrip(int id)
        {
            try
            {
                var company = await _tripRepo.Get(id);
                if (company == null)
                    return NotFound();

                return Ok(company);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateTrip(TripDto model)
        {

            try
            {

                int temp = model.TemperatureC;
                int num = model.TripsNumber;
                TripsNum service = new TripsNum
                {
                    TemperatureC = model.TemperatureC,
                    TripsNumber = model.TripsNumber
                };
                var Data = _mapper.Map<Trip>(model);
                var createdTrip = await _tripRepo.Add(Data , service);

                //_Rabbit.Producer(Data);
               
                return Ok(createdTrip);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, /*ex.Message*/ ex.InnerException.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTrip(TripDto trip)
        {
            try
            {
                var Data = _mapper.Map<Trip>(trip);
                await _tripRepo.Update(Data);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            try
            {
                await _tripRepo.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
