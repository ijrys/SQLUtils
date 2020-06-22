using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MiRaI.SQLUtils {
	public delegate object ExecuterReaderDelegate(SqlDataReader reader);
	public delegate object ExecuterCommandDelegate(SqlConnection conn, SqlCommand cmd);

	public abstract class Executer {

		public string ConnectionString { get; private set; }
		public string SQLScript { get; private set; }
		public SqlConnection SqlConnection { get; protected set; }
		public Dictionary<string, object> Parameters { get; protected set; }

		protected SqlConnection Connection() {
			SqlConnection connection = null;
			if (SqlConnection == null) {
				connection = new SqlConnection(ConnectionString);
			}
			else {
				connection = SqlConnection;
			}
			return connection;
		}

		internal Executer(string connstr, string script, Dictionary<string, object> parameters) {
			ConnectionString = connstr;
			SQLScript = script;
			Parameters = parameters;
		}

		public object Reader(ExecuterReaderDelegate function) {
			using (SqlConnection conn = Connection()) {
				using (SqlCommand cmd = conn.CreateCommand()) {
					cmd.CommandText = SQLScript;
					if (Parameters != null && Parameters.Count != 0) { //Append pairs
						foreach (KeyValuePair<string, object> p in Parameters) {
							cmd.Parameters.AddWithValue(p.Key, p.Value);
						}
					}
					if (conn.State == System.Data.ConnectionState.Closed) {
						conn.Open();
					}

					object re = null;
					using (SqlDataReader reader = cmd.ExecuteReader()) {
						re = function(reader);
					}
					return re;
				}
			}
		}

		public object Command(ExecuterCommandDelegate function) {
			using (SqlConnection conn = Connection()) {
				using (SqlCommand cmd = conn.CreateCommand()) {
					return function(conn, cmd);
				}
			}
		}
	}

	public abstract class Executer<T> : Executer where T : Executer {
		public T SetConnection(SqlConnection connection) {
			SqlConnection = connection;
			return this as T;
		}

		internal Executer(string connstr, string script, Dictionary<string, object> parameters) : base(connstr, script, parameters) {
		}
	}
}
