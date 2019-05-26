using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Weather.Domain.Exceptions;

namespace Weather.Presentation.Classes
{
	public static class OrderingExtensions
	{
		public static IEnumerable<T> OrderByCriteria<T>(this IEnumerable<T> source, SortingCriteria sorting)
		{
			var sortingProperty = typeof(T).GetProperties().FirstOrDefault(x => string.Equals(x.Name, sorting.ColumnName, StringComparison.InvariantCultureIgnoreCase));
			if (sortingProperty == null)
				throw new WeatherValidationException($"{typeof(T).Name} does not have column name '{sorting.ColumnName}'.");

			return sorting.SortOrder == SortOrder.Ascending ? source.OrderBy(x => sortingProperty.GetValue(x)) : source.OrderByDescending(x => sortingProperty.GetValue(x));
		}
	}
}