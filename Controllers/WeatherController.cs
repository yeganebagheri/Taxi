using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Models;

namespace Taxi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        

        [HttpGet("{City}")]
        public int Get(string city)
        {
            var rng = new Random();
            WeatherService res = new WeatherService
            {
                Date = DateTime.Now ,
                TemperatureC = rng.Next(10, 30),
                City = city
            };
            return res.TemperatureC;
        }
    }
}
