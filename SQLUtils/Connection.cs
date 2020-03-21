using System;

namespace MiRaI.SQLUtils {
	public class Connection {
		public string ConnectionString { get; private set; }

		public SelectCommand Select () {
			return new SelectCommand(ConnectionString);
		}
		public SelectCommand Select(string columns) {
			return new SelectCommand(ConnectionString).Select(columns);
		}

		public Connection(string connectionString) {
			ConnectionString = connectionString;
		}

	}
}
