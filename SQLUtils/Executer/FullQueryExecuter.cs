using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MiRaI.SQLUtils {
	public class FullQueryExecuter : Executer<FullQueryExecuter> {
		public delegate object ConvertReaderToEntity(SqlDataReader reader);

		public int NonQuery() {
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

					return cmd.ExecuteNonQuery();
				}
			}
		}

		public object Scalar () {
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

					return cmd.ExecuteScalar();
				}
			}
		}

		public object OneLine(ConvertReaderToEntity converter) {
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
						if (reader.Read()) {
							re = converter(reader);
						}
						if (reader.Read()) {
							throw new Exceptions.LineCountOutOfRangeException();
						}
					}
					return re;
				}
			}
		}

		public object FirstLine(ConvertReaderToEntity converter) {
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
						if (reader.Read()) {
							re = converter(reader);
						}
					}
					return re;
				}
			}
		}

		public FullQueryExecuter(string connstr, string script, Dictionary<string, object> parameters) : base(connstr, script, parameters) {
		}
	}
}
