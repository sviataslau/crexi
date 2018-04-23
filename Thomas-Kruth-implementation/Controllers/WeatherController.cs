using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ThomasKruthimplementation.Models;
using ThomasKruthimplementation.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThomasKruthimplementation.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        WeatherService weatherService;

        public WeatherController(WeatherService wService)
        {
            weatherService = wService;
        }

        [HttpGet]
        [Route("current")]
        public async Task<IActionResult> GetCurrent([FromQuery]string city)
        {
            try
            {
                return Json(await weatherService.GetCurrenWeather(city));
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = "Problem retrieving weather data" });
            }
        }

        [HttpGet]
        [Route("week")]
        public async Task<IActionResult> GetWeekly([FromQuery]string city)
        {
            try
            {
                return Json(await weatherService.GetWeekWeather(city));
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { Message = "Problem retrieving weather data" });
            }
        }

    }
}
