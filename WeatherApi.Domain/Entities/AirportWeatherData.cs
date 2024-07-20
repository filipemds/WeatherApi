using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApi.Domain.Entities
{
    public class AirportWeatherData
    {
        [JsonProperty("codigo_icao")]
        public string CodigoIcao { get; set; }

        [JsonProperty("atualizado_em")]
        public DateTime AtualizadoEm { get; set; }

        [JsonProperty("pressao_atmosferica")]
        public string PressaoAtmosferica { get; set; }

        [JsonProperty("visibilidade")]
        public string Visibilidade { get; set; }

        [JsonProperty("vento")]
        public int Vento { get; set; }

        [JsonProperty("direcao_vento")]
        public int DirecaoVento { get; set; }

        [JsonProperty("umidade")]
        public int Umidade { get; set; }

        [JsonProperty("condicao")]
        public string Condicao { get; set; }

        [JsonProperty("condicao_Desc")]
        public string CondicaoDesc { get; set; }

        [JsonProperty("temp")]
        public int Temp { get; set; }
    }
}
