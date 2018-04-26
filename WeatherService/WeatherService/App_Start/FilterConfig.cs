using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WebApiThrottle;

namespace WeatherService
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
    //        filters.Add(new ThrottlingFilter()
    //        {
    //            Policy = new ThrottlePolicy(perSecond: 1, perMinute: 20,
    //perHour: 200, perDay: 2000, perWeek: 10000)
    //            {
    //                //scope to IPs
    //                IpThrottling = true,
    //                IpRules = new Dictionary<string, RateLimits>
    //    {
    //        { "::1/10", new RateLimits { PerSecond = 2 } },
    //        { "192.168.2.1", new RateLimits { PerMinute = 30, PerHour = 30*60, PerDay = 30*60*24 } }
    //    },
    //                //white list the "::1" IP to disable throttling on localhost
    //                IpWhitelist = new List<string> { "127.0.0.1", "192.168.0.0/24" },

    //                //scope to clients (if IP throttling is applied then the scope becomes a combination of IP and client key)
    //                ClientThrottling = true,
    //                ClientRules = new Dictionary<string, RateLimits>
    //    {
    //        { "api-client-key-demo", new RateLimits { PerDay = 5000 } }
    //    },
    //                //white list API keys that don’t require throttling
    //                ClientWhitelist = new List<string> { "admin-key" },

    //                //Endpoint rate limits will be loaded from EnableThrottling attribute
    //                EndpointThrottling = true
    //            }
    //        });

        }
    }
}
