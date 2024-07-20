using WeatherApi.Domain.Entities;

namespace WeatherApi.Domain.Interfaces
{
    public interface IWeatherRepository
    {
        Task AddWeatherDataAsync(CityWeatherData weatherData);
        Task AddAirportWeatherDataAsync(AirportWeatherData weatherData);
        Task LogErrorAsync(string errorMessage);
    }
}
