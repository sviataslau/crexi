using System.Runtime.Serialization;
using Crexi.WeatherForecast.Shared.Enums;
using Newtonsoft.Json;

namespace Crexi.WeatherForecast.Models
{
	public class ErrorResponse
	{
		[JsonProperty(PropertyName = "response_status")]
		public string StatusMessage { get; set; }

		[JsonProperty(PropertyName = "response_message")]
		public string ErrorMessage { get; set; }

		[IgnoreDataMember]
		public ResponseStatus Status { get; set; }

		public ErrorResponse(ResponseStatus status, string errorMessage = null)
		{
			Status = status;
			StatusMessage = status.ToString();
			ErrorMessage = errorMessage;
		}
	}
}