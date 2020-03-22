using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils {
	public static class SQL {
		public static string DefaultConnectionString { get; set; }

		/// <summary>
		/// get a Connection use DefaultConnectionString
		/// </summary>
		/// <returns></returns>
		public static Connection Connection() {
			return new Connection(DefaultConnectionString);
		}

		/// <summary>
		/// get a Connection use connectionString
		/// </summary>
		/// <param name="connectionString">connectionString</param>
		/// <returns></returns>
		public static Connection Connection (string connectionString) {
			return new Connection(connectionString);
		}
	}
}
