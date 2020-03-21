using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MiRaI.SQLUtils {
	public abstract class Executer {
		public string ConnectionString { get; private set; }
		public string SQLScript { get; private set; }
		public SqlConnection SqlConnection { get; protected set; }
		public Dictionary<string, object> Parameters { get; protected set; }

		protected SqlConnection Connection() {
			SqlConnection connection = null;
			if (SqlConnection == null) {
				connection = new SqlConnection(ConnectionString);
			} else {
				connection = SqlConnection;
			}
			return connection;
		}

		internal Executer(string connstr, string script, Dictionary<string, object> parameters) {
			ConnectionString = connstr;
			SQLScript = script;
			Parameters = parameters;
		}
	}

	public abstract class Executer<T> :Executer where T : Executer {
		public T SetConnection(SqlConnection connection) {
			SqlConnection = connection;
			return this as T;
		}

		internal Executer(string connstr, string script, Dictionary<string, object> parameters) :base(connstr, script, parameters) {
		}
	}
}
