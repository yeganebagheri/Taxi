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
using Taxi.QueryModels;

namespace Taxi.Repository
{
    public class TripRepository : ITripRepository
    {
        private readonly IConfiguration _configuration;
        private int x;
        private int y;
        private readonly IRabbitMqRepository _Rabbit;

        public TripRepository(IConfiguration configuration, IRabbitMqRepository rabbitMq)
        {
            _configuration = configuration;
            _Rabbit = rabbitMq;
        }
       
        public async Task<Trip> Add(Trip entity , TripsNum service)
        {

            var sql = "INSERT INTO Trip (City, Source, Destination, Cost, EndTrip, DriverId, PassengerId, PassengerNationalCode) Values (@City, @Source, @Destination, @Cost, @EndTrip , @DriverId , @PassengerId , @PassengerNationalCode)" +
               "SELECT CAST(SCOPE_IDENTITY() as int)";

            //var rng = new Random();
           
            _Rabbit.Producer(entity);
           // var sqlPassenger = "SELECT * FROM Trip WHERE PassengerNationalCode = @PassengerNationalCode;";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                //var result = await connection.QueryAsync<Trip>(sqlPassenger, new { PassengerNationalCode = entity.PassengerNationalCode });
                //var Number = result.Count();

                int temp = service.TemperatureC;
                int num = service.TripsNumber;
                if (temp < 20)
                {
                    x = 10 + 5;
                    if (num > 5)
                    {
                        x = x - 2;
                    }
                }
                else
                {
                    x = 10;
                }

                int FinalCost = x;

                var parameters = new DynamicParameters();
                parameters.Add(nameof(Trip.City), entity.City, DbType.String);
                parameters.Add(nameof(Trip.Source), entity.Source, DbType.String);
                parameters.Add(nameof(Trip.Destination), entity.Destination, DbType.String);
                parameters.Add(nameof(Trip.Cost), FinalCost , DbType.Int64);
                parameters.Add(nameof(Trip.EndTrip), false, DbType.Boolean);
                parameters.Add(nameof(Trip.DriverId), entity.DriverId, DbType.Int32);
                parameters.Add(nameof(Trip.PassengerId), entity.PassengerId, DbType.Int32);
                parameters.Add(nameof(Trip.PassengerNationalCode), entity.PassengerNationalCode, DbType.Int32);


                var id = await connection.QuerySingleAsync<int>(sql, parameters);
                var CreatedTrip = new Trip
                {
                    Id = id,
                    City = entity.City,
                    Source = entity.Source,
                    Destination = entity.Destination,
                    Cost = FinalCost,
                    EndTrip = false,
                    DriverId = entity.DriverId,
                    PassengerId = entity.PassengerId,
                    PassengerNationalCode = entity.PassengerNationalCode

                };

                return CreatedTrip;
            }

           
        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Trip WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows;

            }
        }

        public async Task<TripQuery> Get(int id)
        {
            var sql = "SELECT * FROM Trip WHERE Id = @Id;";
            //var sql = @"select  m.Id, m.Name,d.Name AS DriverName , p.Name AS PassengerName , d.NationalCode AS DriverNationalCode
            //            , p.NationalCode AS PassengerNationalCode, m.City , m.Source , m.Destination , m.Cost, m.End
            //                    FROM Trip m 
            //                    INNER JOIN Driver d
            //                    ON m.Id = d.Id
            //                    INNER JOIN Passenger p
            //                    ON m.Id = p.Id"; 
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TripQuery>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<TripQuery>> GetAll()
        {
            var sql = @"select  m.Id ,m.DriverId , m.PassengerId ,d.Name AS DriverName , p.Name AS PassengerName , d.NationalCode AS DriverNationalCode
                        , p.NationalCode AS PassengerNationalCode, m.City , m.Source , m.Destination , m.Cost, m.EndTrip
                                FROM Trip m 
                                INNER JOIN Driver d
                                ON m.DriverId = d.Id
                                INNER JOIN Passenger p
                                ON m.PassengerId = p.Id";

            // var sql = "SELECT * FROM Trip;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TripQuery>(sql);
                return result;
            }
        }

        public async Task<int> Update(Trip entity)
        {
            
            var sql = "UPDATE Trip SET City = @City, Source = @Source, Destination = @Destination, Cost = @Cost, End = @End , DriverId = @DriverId , PassengerId = @PassengerId  WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }

       
    }

}
