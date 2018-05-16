using System.Collections.Generic;
using System.Linq;
namespace Vik_WeatherService.Models
{
    public class ResponseModel
    {
        public dynamic Data { get; set; }
        public Dictionary<object, object> Error { get; set; }
        public bool IsValid { get { return !Error?.Any() ?? true; } }
    }
}
