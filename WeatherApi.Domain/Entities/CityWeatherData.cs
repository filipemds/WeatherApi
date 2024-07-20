namespace WeatherApi.Domain.Entities
{
    public class CityWeatherData
    {
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string AtualizadoEm { get; set; }
        public List<WeatherCondition> Clima { get; set; }
    }

    public class WeatherCondition
    {
        public string Data { get; set; }
        public string Condicao { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int IndiceUv { get; set; }
        public string CondicaoDesc { get; set; }
    }
}
