namespace OneMoreWeatherService.SDK.Models
{
    public class WeatherDTO
    {
        public double Degrees { get; set; }
        public bool Clouds { get; set; }
        public bool Wind { get; set; }
        public string WindDirection { get; set; }
        public string Snow { get; set; }
        public string Rain { get; set; }
    }
}
