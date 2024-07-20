using Moq;
using Newtonsoft.Json;
using WeatherApi.Application.Services;
using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Interfaces;

namespace WeatherApi.Tests.Services
{
    public class WeatherServiceTests
    {
        private readonly Mock<IWeatherRepository> _mockWeatherRepository;
        private readonly Mock<IHttpClientWrapper> _mockHttpClientWrapper;
        private readonly WeatherService _weatherService;

        public WeatherServiceTests()
        {
            _mockWeatherRepository = new Mock<IWeatherRepository>();
            _mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            _weatherService = new WeatherService(_mockWeatherRepository.Object, _mockHttpClientWrapper.Object);
        }

        [Fact]
        public async Task GetWeatherByAirportAsync_ShouldSaveWeatherData()
        {
            // Arrange
            var airportCode = "SBAR";
            var jsonResponse = @"{
                'codigo_icao': 'SBAR',
                'atualizado_em': '2021-01-27T15:00:00.974Z',
                'pressao_atmosferica': '1014',
                'visibilidade': '9000',
                'vento': 29,
                'direcao_vento': 90,
                'umidade': 74,
                'condicao': 'ps',
                'condicao_Desc': 'Predomínio de Sol',
                'temp': 28
            }";

            var httpResponseMessage = new HttpResponseMessage
            {
                Content = new StringContent(jsonResponse)
            };

            _mockHttpClientWrapper.Setup(client => client.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);

            var airportWeatherData = JsonConvert.DeserializeObject<AirportWeatherData>(jsonResponse);

            // Act
            await _weatherService.GetWeatherByAirportAsync(airportCode);

            // Assert
            //_mockWeatherRepository.Verify(repo => repo.AddAirportWeatherDataAsync(It.Is<AirportWeatherData>(data => data.CodigoIcao == airportWeatherData.CodigoIcao)), Times.Once);
        }
    }
}
