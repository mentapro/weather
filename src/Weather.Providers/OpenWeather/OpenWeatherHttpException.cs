using Weather.Domain.Exceptions;

namespace Weather.Providers.OpenWeather
{
	internal class OpenWeatherHttpException : WeatherHttpException
	{
		// custom exception for OpenWeather (should be inherited from base exc -> WeatherHttpException)
		public OpenWeatherHttpException(string message, int statusCode) : base(message, statusCode)
		{
		}
	}
}