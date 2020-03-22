using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MiRaI.SQLUtils {
	public class NonQueryExecuter: Executer {
		public NonQueryExecuter(string connstr, string script, Dictionary<string, object> pairs) : base(connstr, script, pairs) {
		}

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

	}
}
