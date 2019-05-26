using System;

namespace Weather.Domain.Exceptions
{
	public class WeatherValidationException : Exception
	{
		public WeatherValidationException(string message) : base(message)
		{
		}
	}
}