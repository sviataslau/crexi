using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace OneMoreWeatherService.SDK.Models
{
    public class WeatherResponce
    {
        [JsonProperty("weather")]
        public IList<WeatherDTO> Weather { get; set; }
    }
}
