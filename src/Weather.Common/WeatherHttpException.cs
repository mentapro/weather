using System;

namespace Weather.Common
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