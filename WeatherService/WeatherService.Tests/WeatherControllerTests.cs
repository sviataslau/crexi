using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using WeatherService.Controllers;
using WeatherService.Models;
using Newtonsoft.Json;
using WeatherService.Models.BindModels;
using Moq;
using WeatherService.Services;

namespace WeatherService.Tests
{
    [TestFixture]
    public class WeatherControllerTests
    {
        private Mock<OpenWeatherMapService> _openWeatherMapMock;

        [SetUp]
        public void WeatherSetup()
        {
            _openWeatherMapMock = new Mock<OpenWeatherMapService>();

        }


        /// <summary>
        /// Tests different cases that could be passed into the city, state, and country
        /// parameters.
        /// GET api/Weather?city=Chico&state=CA&country=USA
        /// </summary>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <param name="shouldSucceed"></param>
        /// <param name="hasData"></param>
        [TestCase("FakeCity","FakeState", "USA", true, HttpStatusCode.NoContent)]
        [TestCase("Chico", "CA", "USA", true, HttpStatusCode.OK)]
        public void Get_CityStateCountry_JsonString(string city, string state, string country
            , bool shouldSucceed, HttpStatusCode expectedStatusCode)
        {
            
            Mock<OpenWeatherMapService> openWeatherMapMock = _openWeatherMapMock;
            WeatherBindModel bindModel = new WeatherBindModel
            {
                City = city,
                State = state,
                Country = country
            };

            WeatherController weatherController = null;
            IHttpActionResult httpActionResult = null;
            Assert.Multiple(() =>
            {
                if (shouldSucceed)
                {
                    // setup the WeatherController and Mock object for OpenWeatherMapService
                    if (expectedStatusCode == HttpStatusCode.NoContent)
                    {
                        openWeatherMapMock.Setup(x => x.FetchWeather<WeatherModel>(city, state, country, "weather"))
                        .Returns<WeatherModel>(null);

                        // check if our result is of type ResponseMessageResult which generates the No Content
                        weatherController = new WeatherController(openWeatherMapMock.Object);
                        httpActionResult = weatherController.Get(bindModel).Result;
                        Assert.IsInstanceOf<ResponseMessageResult>(httpActionResult);
                    }
                    else if (expectedStatusCode == HttpStatusCode.OK)
                    {
                        WeatherModel weatherModel = new WeatherModel
                        {
                            id = 1,
                            name = "fake"
                        };
                        openWeatherMapMock.Setup(x => x.FetchWeather<WeatherModel>(city, state, country, "weather"))
                        .Returns(Task.FromResult<WeatherModel>(weatherModel));

                        // check if our result is of type JsonResult which carries payload back to client
                        weatherController = new WeatherController(openWeatherMapMock.Object);
                        httpActionResult = weatherController.Get(bindModel).Result;
                        Assert.IsInstanceOf<JsonResult<WeatherModel>>(httpActionResult);
                    }
                }
            });
        }

    }
}
