using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils {
	public static class SQL {
		public static string DefaultConnectionString { get; set; }


		public static Connection Connection() {
			return new Connection(DefaultConnectionString);
		}

		public static Connection Connection (string connectionString) {
			return new Connection(connectionString);
		}
	}
}
