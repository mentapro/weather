using System.Data.SqlClient;

namespace Weather.Core.Workers
{
	public class SortingCriteria
	{
		public string ColumnName { get; set; }

		public SortOrder SortOrder { get; set; }
	}
}