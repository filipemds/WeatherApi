namespace WeatherApi.Domain.Entities
{
    public class WeatherData
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string AirportCode { get; set; }
        public float Temperature { get; set; }
        public string Description { get; set; }
        public DateTime RequestTime { get; set; }
    }
}
