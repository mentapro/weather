using Weather.Common;

namespace Weather.Providers.OpenWeather
{
	internal class OpenWeatherHttpException : WeatherHttpException
	{
		public OpenWeatherHttpException(string message, int statusCode) : base(message, statusCode)
		{
		}
	}
}