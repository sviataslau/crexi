namespace Crexi.WeatherForecast.Services.Interfaces
{
	public interface IIpGeolocator
	{
		string GetIpCountryCode(string ip);
	}
}