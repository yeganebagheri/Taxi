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
    public class DriverController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDriverRepository _driverRepo;

        public DriverController( IMapper mapper, IDriverRepository driverRepo)
        {
            _driverRepo = driverRepo;
            _mapper = mapper;
           
        }


        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
               
                var drivers = await _driverRepo.GetAll();
                return Ok(drivers);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var driver = await _driverRepo.Get(id);
                if (driver == null)
                    return NotFound();

                return Ok(driver);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateCompany(DriverDto model)
        {

            try
            {
                var Data = _mapper.Map<Driver>(model);
                var createdTrip = await _driverRepo.Add(Data);
                
                return Ok(createdTrip);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, /*ex.Message*/ ex.InnerException.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany(DriverDto model)
        {
            try
            {
                var Data = _mapper.Map<Driver>(model);
                await _driverRepo.Update(Data);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                await _driverRepo.Delete(id);
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

