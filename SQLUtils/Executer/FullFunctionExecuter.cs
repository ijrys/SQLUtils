using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MiRaI.SQLUtils {


	//public class FullFunctionExecuter : Executer {

	//	public int NonQuery() {
	//		using (SqlConnection conn = Connection()) {
	//			using (SqlCommand cmd = conn.CreateCommand()) {
	//				cmd.CommandText = SQLScript;
	//				if (Parameters != null && Parameters.Count != 0) { //Append pairs
	//					foreach (KeyValuePair<string, object> p in Parameters) {
	//						cmd.Parameters.AddWithValue(p.Key, p.Value);
	//					}
	//				}
	//				if (conn.State == System.Data.ConnectionState.Closed) {
	//					conn.Open();
	//				}

	//				return cmd.ExecuteNonQuery();
	//			}
	//		}
	//	}

	//	public object Scalar() {
	//		using (SqlConnection conn = Connection()) {
	//			using (SqlCommand cmd = conn.CreateCommand()) {
	//				cmd.CommandText = SQLScript;
	//				if (Parameters != null && Parameters.Count != 0) { //Append pairs
	//					foreach (KeyValuePair<string, object> p in Parameters) {
	//						cmd.Parameters.AddWithValue(p.Key, p.Value);
	//					}
	//				}
	//				if (conn.State == System.Data.ConnectionState.Closed) {
	//					conn.Open();
	//				}

	//				return cmd.ExecuteScalar();
	//			}
	//		}
	//	}

	//	/// <summary>
	//	/// 只严格读取最多一行数据。当无数据，返回null。当数据超过一行将抛出异常LineCountOutOfRangeException
	//	/// </summary>
	//	/// <param name="converter"></param>
	//	/// <returns></returns>
	//	public object OneLine(ConvertReaderToEntity converter) {
	//		using (SqlConnection conn = Connection()) {
	//			using (SqlCommand cmd = conn.CreateCommand()) {
	//				cmd.CommandText = SQLScript;
	//				if (Parameters != null && Parameters.Count != 0) { //Append pairs
	//					foreach (KeyValuePair<string, object> p in Parameters) {
	//						cmd.Parameters.AddWithValue(p.Key, p.Value);
	//					}
	//				}
	//				if (conn.State == System.Data.ConnectionState.Closed) {
	//					conn.Open();
	//				}

	//				object re = null;
	//				using (SqlDataReader reader = cmd.ExecuteReader()) {
	//					if (reader.Read()) {
	//						re = converter(reader);
	//					}
	//					if (reader.Read()) {
	//						throw new Exceptions.LineCountOutOfRangeException();
	//					}
	//				}
	//				return re;
	//			}
	//		}
	//	}

	//	/// <summary>
	//	/// 读取最多一行数据。当无数据，返回null。当数据超过一行将只读取第一行
	//	/// </summary>
	//	/// <param name="converter"></param>
	//	/// <returns></returns>
	//	public object FirstLine(ConvertReaderToEntity converter) {
	//		using (SqlConnection conn = Connection()) {
	//			using (SqlCommand cmd = conn.CreateCommand()) {
	//				cmd.CommandText = SQLScript;
	//				if (Parameters != null && Parameters.Count != 0) { //Append pairs
	//					foreach (KeyValuePair<string, object> p in Parameters) {
	//						cmd.Parameters.AddWithValue(p.Key, p.Value);
	//					}
	//				}
	//				if (conn.State == System.Data.ConnectionState.Closed) {
	//					conn.Open();
	//				}

	//				object re = null;
	//				using (SqlDataReader reader = cmd.ExecuteReader()) {
	//					if (reader.Read()) {
	//						re = converter(reader);
	//					}
	//				}
	//				return re;
	//			}
	//		}
	//	}

	//	public List<T> Lines<T>(ConvertReaderToEntity converter) {
	//		using (SqlConnection conn = Connection()) {
	//			using (SqlCommand cmd = conn.CreateCommand()) {
	//				cmd.CommandText = SQLScript;
	//				if (Parameters != null && Parameters.Count != 0) { //Append pairs
	//					foreach (KeyValuePair<string, object> p in Parameters) {
	//						cmd.Parameters.AddWithValue(p.Key, p.Value);
	//					}
	//				}
	//				if (conn.State == System.Data.ConnectionState.Closed) {
	//					conn.Open();
	//				}

	//				List<T> re = new List<T>();
	//				T obj = default(T);
	//				using (SqlDataReader reader = cmd.ExecuteReader()) {
	//					if (reader.Read()) {
	//						obj = (T)converter(reader);
	//						re.Add(obj);
	//					}
	//				}
	//				return re;
	//			}
	//		}
	//	}



	//	public FullFunctionExecuter(string connstr, string script, Dictionary<string, object> parameters) : base(connstr, script, parameters) {
	//	}
	//}
}
