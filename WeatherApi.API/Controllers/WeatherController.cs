using Microsoft.AspNetCore.Mvc;
using WeatherApi.Application.Services;
using System.Threading.Tasks;

namespace WeatherApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        const string erroExecucao = "Erro ao executar busca";

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }


        /// <summary>
        /// Obtém o clima de uma cidade.
        /// </summary>
        /// <param name="city">Nome da cidade.</param>
        /// <returns>Dados climáticos da cidade.</returns>
        [HttpGet("city/{city}")]
        public async Task<IActionResult> GetWeatherByCity(string city)
        {
            try
            {
                var weather = await _weatherService.GetWeatherByCityAsync(city);
                return Ok(weather);
            }
            catch
            {
                return StatusCode(500, erroExecucao);
            }
        }

        /// <summary>
        /// Obtém o clima de um aeroporto.
        /// </summary>
        /// <param name="icaoCode">Código ICAO do aeroporto.</param>
        /// <returns>Dados climáticos do aeroporto.</returns>
        [HttpGet("airport/{icaoCode}")]
        public async Task<IActionResult> GetWeatherByAirport(string icaoCode)
        {
            try
            {
                var weather = await _weatherService.GetWeatherByAirportAsync(icaoCode);
                return Ok(weather);
            }
            catch
            {
                return StatusCode(500, erroExecucao);
            }
        }
    }
}
