using System.Data.SqlClient;

namespace Weather.Presentation.Classes
{
	public class SortingCriteria
	{
		public string ColumnName { get; set; }

		public SortOrder SortOrder { get; set; }
	}
}