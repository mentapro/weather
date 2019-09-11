using Weather.Domain.Exceptions;

namespace Weather.Providers
{
	internal class WeatherProviderHttpException : WeatherHttpException
	{
		// custom exception for Weather providers (should be inherited from base exc -> WeatherHttpException)
		public WeatherProviderHttpException(string message, int statusCode) : base(message, statusCode)
		{
		}
	}
}