using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Interfaces;

namespace WeatherApi.Infrastructure.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly string _connectionString;

        public WeatherRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddWeatherDataAsync(CityWeatherData cityWeatherData)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO WeatherData (City, Temperature, Description, RequestTime) VALUES (@City, @Temperature, @Description, @RequestTime)";
                await db.ExecuteAsync(query, cityWeatherData);
            }
        }

        public async Task AddAirportWeatherDataAsync(AirportWeatherData airportWeatherData)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = @"INSERT INTO WeatherData (CodigoIcao, AtualizadoEm, PressaoAtmosferica, Visibilidade, Vento, DirecaoVento, Umidade, Condicao, CondicaoDesc, Temp)
                                   VALUES (@CodigoIcao, @AtualizadoEm, @PressaoAtmosferica, @Visibilidade, @Vento, @DirecaoVento, @Umidade, @Condicao, @CondicaoDesc, @Temp)";

                await db.ExecuteAsync(query, airportWeatherData);
            }
        }

        public async Task LogErrorAsync(string errorMessage)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO ErrorLogs (ErrorMessage, LogTime) VALUES (@ErrorMessage, @LogTime)";
                await db.ExecuteAsync(query, new { ErrorMessage = errorMessage, LogTime = DateTime.Now });
            }
        }
    }
}
