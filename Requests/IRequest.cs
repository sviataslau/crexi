using System;
using Microsoft.Extensions.Logging;
using Vik_WeatherService.Redis;
using System.Net.Http;
using System.Threading.Tasks;
using Vik_WeatherService.Models;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using Microsoft.Extensions.Options;

namespace Vik_WeatherService.Requests
{
    public interface IRequest
    {
        Task<dynamic> HttpApiRequestAsync(string cityName, dynamic parameter);
    }
    public class RequestImp : IRequest
    {
        private readonly ILogger<RequestImp> _logger;
        private readonly IDatabase _redis;
        private readonly IHttpClient _httpClient;
        private readonly IOptions<WeatherApiConfig> _configuration;
        public RequestImp(ILogger<RequestImp> logger, IRedisConnectionProvider redis, IHttpClient httpClient, IOptions<WeatherApiConfig> configuration)
        {
            _logger = logger;
            _redis = redis.Connection().GetDatabase();
            _httpClient = httpClient;
            _configuration = configuration;
            
        }

        public async Task<dynamic> HttpApiRequestAsync(string cityName, dynamic parameter)
        {
            var weatherApiUri = _configuration.Value.WeatherApiHost;
            var duration = parameter as string;
            var cachedData = string.Empty;
            switch (duration?.ToLower())
            {
                case "today":
                    weatherApiUri = $"{weatherApiUri}weather?q={cityName}&APPID={_configuration.Value.WeatherApiKey}";
                    break;
                case "week":
                    weatherApiUri = $"{weatherApiUri}forecast?q={cityName}&cnt=7&APPID={_configuration.Value.WeatherApiKey}";
                    break;
            }
            if (_redis.IsConnected(weatherApiUri))
                cachedData = await _redis.StringGetAsync(weatherApiUri);
            if (string.IsNullOrEmpty(cachedData))
            {
                var httpmessage = new HttpRequestMessage
                {
                    RequestUri = new Uri(weatherApiUri),
                    Method = HttpMethod.Get
                };

                try
                {
                    var response = await _httpClient.HttpClient.SendAsync(httpmessage);
                    if (response?.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        if (_redis.IsConnected(weatherApiUri))
                            _redis.StringSet(weatherApiUri, content);//cache the value
                        dynamic json = JObject.Parse(content);

                        var responseModel = new ResponseModel { Data = json, Error = new System.Collections.Generic.Dictionary<object, object>() };
                        return responseModel;
                    }
                    else
                    {
                        var error = new
                        {
                            ErrorCode = response.StatusCode,
                            ErrorMessage = await response?.Content?.ReadAsStringAsync()
                        };
                        var errorModel = new ResponseModel
                        {
                            Data = null,
                            Error =
                            new System.Collections.Generic.Dictionary<object, object>() {
                            { error.ErrorCode, error.ErrorMessage } }
                        };
                        return errorModel;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                    //Handled by middleware
                }
            }
            else
            {
                dynamic json = JObject.Parse(cachedData);

                var responseModel = new ResponseModel { Data = json, Error = new System.Collections.Generic.Dictionary<object, object>() };
                return responseModel;
            }
        }

    }
}