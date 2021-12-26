using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Context;
using Taxi.Contracts;
using Taxi.Models;

namespace Taxi.Repository
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly IConfiguration _configuration;
        private readonly DapperContext _context;

        public PassengerRepository(IConfiguration configuration , DapperContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<Passenger> Add(Passenger entity)
        {
            var sql = "INSERT INTO Passenger (Name, NationalCode, Gender) Values (@Name, @NationalCode, @Gender)" +
               "SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();
            parameters.Add(nameof(Passenger.Name), entity.Name, DbType.String);
            parameters.Add(nameof(Passenger.NationalCode), entity.NationalCode, DbType.Int32);
            parameters.Add(nameof(Passenger.Gender), entity.Gender, DbType.String);


            using (var connection = _context.CreateConnection())
            {
               
                var id = await connection.QuerySingleAsync<int>(sql, parameters);

                var createdCompany = new Passenger
                {
                    Id = id,
                    Name = entity.Name,
                    NationalCode = entity.NationalCode,
                    Gender = entity.Gender
                    
                };

                return createdCompany;
                
               // var SqlRows = await connection.ExecuteAsync(sql, entity);
                //return SqlRows;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Passenger WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows;

            }
        }

        public async Task<Passenger> Get(int id)
        {
            var sql = @"select  m.Id, m.Name, m.NationalCode , m.Gender 
                                FROM Passenger m ";
                               
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Passenger>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Passenger>> GetAll()
        {
            var sql = @"select  m.Id, m.Name, m.NationalCode , m.Gender 
                                FROM Passenger m ";
            // var sql = "SELECT * FROM Trip;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Passenger>(sql);
                return result;
            }
        }

        public async Task<int> Update(Passenger entity)
        {
            var sql = "UPDATE Passenger SET Name = @Name, NationalCode = @NationalCode, Gender = @Gender  WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }
    }
}
