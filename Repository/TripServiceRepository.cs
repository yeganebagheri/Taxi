using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Contracts;
using Taxi.Models;

namespace Taxi.Repository
{
    public class TripServiceRepository : ITripServiceRepository
    {
        private readonly IRabbitMqRepository _Rabbit;
        private readonly IConfiguration _configuration;

        public TripServiceRepository(IConfiguration configuration , IRabbitMqRepository rabbitMq)
        {
            _configuration = configuration;
            _Rabbit = rabbitMq;
        }

        public async Task Update(int id)
        {
            var sqlId = "SELECT * FROM Trip WHERE Id = @Id;";
            var sql = "UPDATE Trip SET EndTrip = @EndTrip  WHERE Id = @Id;";
           
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                var parameter = new DynamicParameters();
                parameter.Add(nameof(Trip.EndTrip), true , DbType.Boolean);
                parameter.Add(nameof(Trip.Id), id , DbType.Int32);

                await connection.ExecuteAsync(sql, parameter);
                
                var result = await connection.QueryAsync<Trip>(sqlId, new { Id = id });
                var result1 = result.FirstOrDefault();
                _Rabbit.Producer(result1);

               

            }

            
        }

       

        public async Task<int> Get(int NationalCode)
        {
            var sql = "SELECT * FROM Trip WHERE PassengerNationalCode = @PassengerNationalCode;";
            
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Trip>(sql, new { PassengerNationalCode = NationalCode });
                return result.Count();
            }
        }
    }
}

