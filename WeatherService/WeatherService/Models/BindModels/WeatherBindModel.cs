using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherService.Models.BindModels
{
    public class WeatherBindModel
    {
        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }
}