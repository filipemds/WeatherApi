using Dapper;
using Moq;
using System.Data;
using WeatherApi.Domain.Entities;
using WeatherApi.Infrastructure.Repositories;

namespace WeatherApi.Tests.Repositories
{
    public class WeatherRepositoryTests
    {
        private readonly Mock<IDbConnection> _mockDbConnection;
        private readonly WeatherRepository _repository;

        public WeatherRepositoryTests()
        {
            _mockDbConnection = new Mock<IDbConnection>();

        }

        [Fact]
        public async Task AddWeatherDataAsync_ShouldAddWeatherData()
        {
            // Arrange
            var weatherData = new AirportWeatherData
            {
                CodigoIcao = "SBAR",
                AtualizadoEm = DateTime.UtcNow,
                PressaoAtmosferica = "1014",
                Visibilidade = "9000",
                Vento = 29,
                DirecaoVento = 90,
                Umidade = 74,
                Condicao = "ps",
                CondicaoDesc = "Predomínio de Sol",
                Temp = 28
            };

            _mockDbConnection.Setup(conn => conn.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);

            // Act
            //await _repository.AddWeatherDataAsync(weatherData);

            // Assert
            _mockDbConnection.Verify(conn => conn.ExecuteAsync(It.IsAny<string>(), weatherData, null, null, null), Times.Once);
        }
    }
}
