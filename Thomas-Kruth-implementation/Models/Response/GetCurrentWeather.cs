using System;
namespace ThomasKruthimplementation.Models.Response
{
    public class GetCurrentWeather
    {
        public GetCurrentWeather()
        {
        }

        public string City { get; set; }
        public decimal Temp { get; set; }
        public string Measurement { get; set; }
    }
}
