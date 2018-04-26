using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Models;
using WeatherService.Services;

namespace WeatherService.Tests
{
    [TestFixture]
    public class OpenWeatherMapServiceTests
    {
        public OpenWeatherMapService _openWeatherMapService;

        [SetUp]
        public void ServiceSetup()
        {
            _openWeatherMapService = new OpenWeatherMapService();
        }

        [TestCase("Chico", "CA", "USA", "weather", true)]
        [TestCase("Los Angeles", "CA", "USA", "forecast", true)]
        [TestCase("London", null, "UK", "weather", true)]
        [TestCase("FakeCity", "FK", "FAKE", "weather", false)]
        [TestCase("FakeCity", "FK", "FAKE", "forecast", false)]
        [TestCase(null, "CA", "USA", "weather", true)]
        public async Task FetchWeather_DataDrivenCases(string city, string state, string country,
            string weatherMode, bool shouldSucceed)
        {
            OpenWeatherMapService openWeatherMapService = _openWeatherMapService;

            if (weatherMode.Equals("weather"))
            {
                WeatherModel model = await openWeatherMapService
                    .FetchWeather<WeatherModel>(city, state, country, weatherMode);

                if (shouldSucceed)
                {
                    Assert.IsNotNull(model);
                }
                else
                {
                    Assert.IsNull(model);
                }
            }
            else
            {
                ForecastModel model = await openWeatherMapService
                    .FetchWeather<ForecastModel>(city, state, country, weatherMode);

                if (shouldSucceed)
                {
                    Assert.IsNotNull(model);
                }
                else
                {
                    Assert.IsNull(model);
                }

            }
        }

    }
}
