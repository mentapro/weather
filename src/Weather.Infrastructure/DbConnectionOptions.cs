namespace Weather.Infrastructure
{
	public class DbConnectionOptions
	{
		public string Host { get; set; }

		public string User { get; set; }

		public string Password { get; set; }

		public string DbName { get; set; }

		public string ConnectionString =>
			$"Server={Host};Uid={User};Pwd={Password};Database={DbName};AllowBatch=True;Allow User Variables=True;CharSet=utf8;";
	}
}