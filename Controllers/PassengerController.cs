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
    public class PassengerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPassengerRepository _passengerRepo;

        public PassengerController(IMapper mapper, IPassengerRepository passengerRepo)
        {
            _passengerRepo = passengerRepo;
            _mapper = mapper;

        }


        [HttpGet]
        public async Task<IActionResult> GetPassengers()
        {
            try
            {
                
                var drivers = await _passengerRepo.GetAll();
                return Ok(drivers);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPassenger(int id)
        {
            try
            {
                var passenger = await _passengerRepo.Get(id);
                if (passenger == null)
                    return NotFound();

                return Ok(passenger);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreatePassenger(PassengerDto model)
        {

            try
            {
                var Data = _mapper.Map<Passenger>(model);
                var createdTrip = await _passengerRepo.Add(Data);

                return Ok(createdTrip);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, /*ex.Message*/ ex.InnerException.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePassenger(PassengerDto model)
        {
            try
            {
                var Data = _mapper.Map<Passenger>(model);
                await _passengerRepo.Update(Data);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassenger(int id)
        {
            try
            {
                await _passengerRepo.Delete(id);
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
