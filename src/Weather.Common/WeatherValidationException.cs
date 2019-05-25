using System;

namespace Weather.Common
{
	public class WeatherValidationException : Exception
	{
		public WeatherValidationException(string message) : base(message)
		{
		}
	}
}