using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using WeatherService.Models;
using WeatherService.Models.BindModels;
using WeatherService.Services;
using WebApiThrottle;

namespace WeatherService.Controllers
{
    [RoutePrefix("api/Weather")]
    public class WeatherController : ApiController
    {
        IWeatherService _openWeatherService;

        public WeatherController()
        {
            _openWeatherService = new OpenWeatherMapService();
        }

        public WeatherController(IWeatherService weatherService)
        {
            _openWeatherService = weatherService;
        }

        /// <summary>
        /// Produces the current weather for the given city and country combination.  The
        /// state parameter is optional for international locations.
        /// GET api/Weather?city=Chico&state=CA&country=USA
        /// </summary>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpGet]
        [EnableThrottling(PerSecond = 1, PerMinute = 3, PerHour = 100)]
        public async Task<IHttpActionResult> Get([FromUri] WeatherBindModel weatherBm)
        {
            if (weatherBm == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(weatherBm.City) || string.IsNullOrEmpty(weatherBm.Country))
            {
                // Required parameters not present. Return Unprocessable Entity status code.
                HttpResponseMessage response = Request.CreateErrorResponse((HttpStatusCode)422,
                    new HttpError("city and country are required."));

                return new ResponseMessageResult(response);
            }

            // go get the data
            WeatherModel weather = await _openWeatherService
                .FetchWeather<WeatherModel>(weatherBm.City, weatherBm.State, weatherBm.Country, "weather");
            if (weather == null)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);
                return new ResponseMessageResult(response);
            }
            return Json(weather);
        }

        /// <summary>
        /// Produces the 5-Day Forecast for a given city and country combination.  The 
        /// state parameter is optional for international locations.
        /// GET api/Weather/Forecast?city=Chico&state=CA&country=USA
        /// </summary>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Forecast")]
        [EnableThrottling(PerSecond = 1, PerMinute = 2, PerHour = 100)]
        public async Task<IHttpActionResult> GetForecast([FromUri] WeatherBindModel weatherBm)
        {
            if (weatherBm == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(weatherBm.City) || string.IsNullOrEmpty(weatherBm.Country))
            {
                // Required parameters not present. Return Unprocessable Entity status code.
                HttpResponseMessage response = Request.CreateErrorResponse((HttpStatusCode)422,
                    new HttpError("city and country are required."));

                return new ResponseMessageResult(response);
            }

            // go get the data
            ForecastModel forecast = await _openWeatherService
                .FetchWeather<ForecastModel>(weatherBm.City, weatherBm.State, weatherBm.Country, "forecast");
            if (forecast == null)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);
                return new ResponseMessageResult(response);
            }

            return Json(forecast);
        }

    }
}
