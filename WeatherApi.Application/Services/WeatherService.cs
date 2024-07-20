using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using WeatherApi.Application.Models;
using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Interfaces;

namespace WeatherApi.Application.Services
{
    public class WeatherService
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IHttpClientWrapper _httpClientWrapper;

        public WeatherService(IWeatherRepository weatherRepository, IHttpClientWrapper httpClientWrapper)
        {
            _weatherRepository = weatherRepository;
            _httpClientWrapper = httpClientWrapper;
        }


        public async Task<CityWeatherData> GetWeatherByCityAsync(string city)
        {
            try
            {
                int cityCode = await GetCityCodeByNameAsync(city);

                var response = await _httpClientWrapper.GetAsync($"https://brasilapi.com.br/api/cptec/v1/clima/previsao/{cityCode}");
                var responseString = await response.Content.ReadAsStringAsync();
                var cityWeather = JsonConvert.DeserializeObject<CityWeatherData>(responseString);

                // Process and save the data
                foreach (var condition in cityWeather.Clima)
                {
                    var cityWeatherData = new CityWeatherData
                    {
                        Cidade = cityWeather.Cidade,
                        Clima = cityWeather.Clima,
                        AtualizadoEm = cityWeather.AtualizadoEm,
                        Estado = cityWeather.Estado
                    };

                    //await _weatherRepository.AddWeatherDataAsync(cityWeatherData);
                }

                return cityWeather;
            }
            catch (Exception ex)
            {
                await _weatherRepository.LogErrorAsync(ex.Message);
                throw;
            }
        }


        public async Task<AirportWeatherData> GetWeatherByAirportAsync(string icaoCode)
        {
            try
            {
                var response = await _httpClientWrapper.GetAsync($"https://brasilapi.com.br/api/cptec/v1/clima/aeroporto/{icaoCode}");
                var responseString = await response.Content.ReadAsStringAsync();
                var airportWeather = JsonConvert.DeserializeObject<AirportWeatherData>(responseString);

                var airportWeatherData = new AirportWeatherData
                {
                    CodigoIcao = airportWeather.CodigoIcao,
                    Condicao = airportWeather.Condicao,
                    PressaoAtmosferica = airportWeather.PressaoAtmosferica,
                    Temp = airportWeather.Temp,
                    Umidade = airportWeather.Umidade,
                    DirecaoVento = airportWeather.DirecaoVento,
                    AtualizadoEm = airportWeather.AtualizadoEm,
                    CondicaoDesc = airportWeather.CondicaoDesc,
                    Vento = airportWeather.Vento,
                    Visibilidade = airportWeather.Visibilidade
                };

                //await _weatherRepository.AddAirportWeatherDataAsync(airportWeatherData);
                return airportWeather;
            }
            catch (Exception ex)
            {
                await _weatherRepository.LogErrorAsync(ex.Message);
                throw;
            }
        }

        public async Task<int> GetCityCodeByNameAsync(string cityName)
        {
            try
            {
                var response = await _httpClientWrapper.GetAsync($"https://brasilapi.com.br/api/cptec/v1/cidade/{cityName}");
                var responseString = await response.Content.ReadAsStringAsync();
                var cityCodeResponse = JsonConvert.DeserializeObject<CityCodeResponse[]>(responseString);

                if (cityCodeResponse.Length > 0)
                {
                    return cityCodeResponse[0].Id;
                }

                throw new Exception("City not found");
            }
            catch (Exception ex)
            {
                await _weatherRepository.LogErrorAsync(ex.Message);
                throw;
            }
        }

        public async Task<List<Airport>> GetAirportsByCityAsync(string airportCity)
        {
            try
            {
                var response = await _httpClientWrapper.GetAsync($"https://aviationapi.com/v1/airports?city={airportCity}");
                var responseString = await response.Content.ReadAsStringAsync();
                var airports = JsonConvert.DeserializeObject<List<Airport>>(responseString);
                return airports;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
