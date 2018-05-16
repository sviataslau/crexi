using System.Collections.Generic;

namespace Vik_WeatherService.Models
{
    public static class RateLimitSettings
    {
        public static int interval = 30;
        public static Dictionary<string, int> numberOfTriesFor30min = new Dictionary<string, int>()
        { {"today",5},{"week",1}};// can have all the rateLimits designed here
    }
}
