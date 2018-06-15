using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Crexi.WeatherForecast.Common.Serialization
{
	public static class ObjectSerializer
	{
		public static string ToJson<T>(T value, bool ignoreNullValues = false)
		{
			var settings = new JsonSerializerSettings
			{
				Formatting = Formatting.None,
				NullValueHandling = ignoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include
			};

			return JsonConvert.SerializeObject(value, settings);
		}

		public static string ToString<T>(T value, bool ignoreNullValues = true)
		{
			return ignoreNullValues
				? JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
				: JsonConvert.SerializeObject(value);
		}

		public static T ToObject<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}
	}
}
