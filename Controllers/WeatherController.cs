using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vik_WeatherService.Requests;

namespace Vik_WeatherService.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private readonly IRequest _request;
        public WeatherController(IRequest request)
        {

            _request = request;
        }
        /// <summary>
        /// To test Start up
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/")]
        public IActionResult GetWeather()
        {   
            return new OkObjectResult("WeatherApi");
        }


        /// <summary>
        /// Retrieves today's weather
        /// please refer this to get list of cityNames's
        /// https://openweathermap.org/current#name
        /// </summary>
        [HttpGet]
        [Route("today")]
        public async Task<IActionResult> GetWeatherForToday([FromQuery]string cityName)
        {
            if (string.IsNullOrEmpty(cityName))
                throw new ArgumentNullException($"cityName cannot be null");
            var response = await _request.HttpApiRequestAsync(cityName, "Today");
            return new ObjectResult(response);
        }
        ///<summary>
        /// Retreives whole week's weather
        ///
        [HttpGet]
        [Route("week")]
        public async Task<IActionResult> GetWeatherForWeek([FromQuery]string cityName)
        {

            if (string.IsNullOrEmpty(cityName))
                throw new ArgumentNullException($"cityName cannot be null");
            var response = await _request.HttpApiRequestAsync(cityName, "Week");
            return new ObjectResult(response);
        }

    }
}
