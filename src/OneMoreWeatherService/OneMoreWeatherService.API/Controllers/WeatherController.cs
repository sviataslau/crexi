using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OneMoreWeatherService.SDK.Interfaces;
using System;

namespace OneMoreWeatherService.API.Controllers
{
    [Route("api/Weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherEngine _weatherEngine;

        public WeatherController(IWeatherEngine weatherEngine)
        {
            //used ctor injection
            _weatherEngine = weatherEngine;
        }

        [HttpGet("Week/{City}")]
        public ActionResult<IEnumerable<string>> GetWeekWeather(string City)
        {
            var result = _weatherEngine.GetWeekWeatherByCity(City);
            throw new Exception("unimplemented mapping from DTO to responce ((");
        }

        [HttpGet("Today/{City}")]
        public ActionResult<string> GetTodayWeather(string City)
        {
            var result = _weatherEngine.GetTodayWeatherByCity(City);
            throw new Exception("unimplemented mapping from DTO to responce ((");
        }
    }
}