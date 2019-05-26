using System;

namespace Weather.Domain.Exceptions
{
	public class WeatherHttpException : Exception
	{
		public int StatusCode { get; }

		public WeatherHttpException(string message, int statusCode) : base(message)
		{
			StatusCode = statusCode;
		}
	}
}