using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Contracts;

namespace Taxi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripServiceController : ControllerBase
    {
        private readonly ITripServiceRepository _service;

        public TripServiceController(ITripServiceRepository service)
        {

            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetNumberTrip(int NationalCode)
        {
            try
            {

                int number = await _service.Get(NationalCode);
                if (number == 0)
                    return NotFound();

                return Ok(number);

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> EndTrip(int id)
        {
            try
            {

                await _service.Update(id);               
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
