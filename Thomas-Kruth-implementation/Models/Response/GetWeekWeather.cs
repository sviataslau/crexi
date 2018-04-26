using System;
using System.Collections.Generic;

namespace ThomasKruthimplementation.Models.Response
{
    public class GetWeekWeather
    {
        public GetWeekWeather()
        {
            WeekData = new List<Weather>();
        }
        public string City { get; set; }
        public List<Weather> WeekData { get; set; }
    }

    public class Weather {
        public string Date { get; set; }
        public decimal Temp { get; set; }
        public string Measurement { get; set; }
    }
}
