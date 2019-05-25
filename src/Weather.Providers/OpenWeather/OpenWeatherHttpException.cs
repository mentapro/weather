using System;

namespace Weather.Providers.OpenWeather
{
	public class OpenWeatherHttpException : Exception
	{
		public int StatusCode { get; }

		public OpenWeatherHttpException(string message, int statusCode) : base(message)
		{
			StatusCode = statusCode;
		}
	}
}