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
using Taxi.Dto;
using Taxi.Models;
using Taxi.QueryModels;

namespace Taxi.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private readonly IConfiguration _configuration;
        private readonly DapperContext _context;

        public DriverRepository(IConfiguration configuration, DapperContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<Driver> Add(Driver entity)
        {
            var sql = "INSERT INTO Driver (Name, NationalCode, Gender, Birthday , CarTage , CarName , Passengerable ) Values (@Name, @NationalCode, @Gender, @Birthday , @CarTage , @CarName , @Passengerable)" +
               "SELECT CAST(SCOPE_IDENTITY() as int)";

            var CarSql = "INSERT INTO Car (DriverId , CarTage) Values (@DriverId, @CarTage )" +
               "SELECT CAST(SCOPE_IDENTITY() as int)";

            var CarTypeSql = $"INSERT INTO CarType (CarId, Passengerable,  CarName) Values (@CarId , @Passengerable, @CarName )" +
              "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add(nameof(Driver.Name), entity.Name, DbType.String);
            parameters.Add(nameof(Driver.NationalCode), entity.NationalCode, DbType.Int32);
            parameters.Add(nameof(Driver.Gender), entity.Gender, DbType.String);
            parameters.Add(nameof(Driver.Birthday), entity.Birthday, DbType.DateTime);
            parameters.Add(nameof(Driver.CarTage), entity.CarTage, DbType.Int32);
            parameters.Add(nameof(Driver.CarName), entity.CarName, DbType.String);
            parameters.Add(nameof(Driver.Passengerable), entity.Passengerable, DbType.Boolean);


            using (var connection = _context.CreateConnection())
            {
                //connection.Open();
                var id = await connection.QuerySingleAsync<int>(sql, parameters);
            var CreatedAccount = new Driver
            {
                Id = id,
                Name = entity.Name,
                NationalCode = entity.NationalCode,
                Gender = entity.Gender,
                Birthday = entity.Birthday,
                CarTage = entity.CarTage,
                CarName = entity.CarName,
                Passengerable = entity.Passengerable

            };
            var CarParameters = new DynamicParameters();
            CarParameters.Add(nameof(Car.DriverId), id, DbType.Int32);
            CarParameters.Add(nameof(Car.CarTage), entity.CarTage , DbType.Int32);
            var CarId = await connection.QuerySingleAsync<int>(CarSql, CarParameters);


            var CarTypeParameters = new DynamicParameters();
            CarTypeParameters.Add(nameof(CarType.CarId), CarId , DbType.Int32);
            CarTypeParameters.Add(nameof(CarType.CarName), entity.CarName, DbType.String);
            CarTypeParameters.Add(nameof(CarType.Passengerable), entity.Passengerable, DbType.Boolean);
            var walletId = await connection.QuerySingleAsync<int>(CarTypeSql, CarTypeParameters);


                return CreatedAccount;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Driver WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows;

            }
        }

        public async Task<DriverQuery> Get(int id)
        {
           
            var sql = "SELECT * FROM Driver WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<DriverQuery>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<DriverDto>> GetAll()
        {
            //var sql = @"select  m.Id, m.Name, c.CarTage AS CarTage , m.NationalCode , m.Gender , m.Birthday , m.Cost, m.End
            //                    FROM Driver m 
            //                    INNER JOIN Car c
            //                    ON m.Id = c.Id";
            var sql = "SELECT * FROM Driver;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<DriverDto>(sql);
                return result;
            }
        }

        public async Task<int> Update(Driver entity)
        {
            var sql = "UPDATE Driver SET Name = @Name, NationalCode = @NationalCode, Gender = @Gender, Birthday = @Birthday, Cost = @Cost , End = @End  WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }
    }
}
