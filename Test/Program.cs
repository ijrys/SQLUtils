using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MiRaI.SQLUtils;

namespace Test {
	class Program {
		static void Main(string[] args) {
			SQL.DefaultConnectionString = "server=.\\SQLEXPRESS;database=MiRaIUser;User Id=miraiadmin;Password=123456";
			//InsertTest();
			//ProcessTest();
			SelectLines();
			//SelectTest();
		}

		static void ProcessTest() {
			var cmd = SQL.Connection()
				.Process().From("dbo", "RegUserTmpKey");
			Console.WriteLine(cmd.CommandScript());
			int re = cmd
				.Execute(1L, 2L, 3)
				.NonQuery();

			Console.WriteLine(re);
		}

		static void ScriptTest() {
			string script = "select * from [User]";
			Dictionary<string, object> values = SQL.Connection()
				.Script(script)
				.Execute()
				.Reader((SqlDataReader reader) => {
					Dictionary<string, object> values = new Dictionary<string, object>();
					if (reader.Read()) {

						for (int i = 0; i < reader.FieldCount; i++) {
							values[reader.GetName(i)] = reader.GetValue(i);
						}
					}
					return values;
				}) as Dictionary<string, object>;

			foreach (var item in values) {
				Console.WriteLine($"{item.Key}    \t | {item.Value}");
			}
		}

		static void DeleteTest() {
			int re = SQL.Connection()
				.Delete().From("User")
				.SetSafetyCheck(false)
				.AddParameter("id", 3)
				.Execute()
				.NonQuery();
			Console.WriteLine($"{re} row(s) changed");
		}

		static void InsertTest() {
			int re = SQL.Connection()
				.Insert().From("User")
				.Columns("Account", "Password", "NickName", "Photo", "Email", "Phone", "State", "RegistDate", "Remarks")
				.Value("test003", "003pwd", "test", "", "", "", (short)3, DateTime.Now, "test 03")
				.Execute()
				.NonQuery();
			Console.WriteLine($"{re} row(s) changed");
		}

		static void UpdateTest() {
			SelectTest();

			int re = SQL.Connection()
				.Update().From("User")
				.AppendSet("nickname", "sqlutils")
				.SetSafetyCheck(false)
				.AddParameter("id", 1)
				.Execute()
				.NonQuery();
			Console.WriteLine($"{re} row(s) changed");

			SelectTest();
		}

		static void SelectTest() {
			Dictionary<string, object> values = SQL.Connection()
				.Select()
				.FromAs("User", "a")
				//.Outer.Join(
				//	new SelectCommand()
				//	.Select()
				//	.From("UserTmpKey")
				//	.Where("[UID] < 5"), "b")
				//.On("a.Id = b.UID")
				.Where("a.[Id] = @id")
				.AddParameter("id", 1)
				.Execute()
				.FirstLine((SqlDataReader reader) => {
					Dictionary<string, object> values = new Dictionary<string, object>();
					for (int i = 0; i < reader.FieldCount; i++) {
						values[reader.GetName(i)] = reader.GetValue(i);
					}
					return values;
				}) as Dictionary<string, object>;

			foreach (var item in values) {
				Console.WriteLine($"{item.Key}    \t | {item.Value}");
			}
		}

		static void SelectLines() {
			List<int> ids = SQL.Connection()
				.Select().From("User")
				.Execute().Lines<int>(
					(SqlDataReader reader, out bool append) => {
						Console.WriteLine($"{reader["ID"]} - {reader["Account"]}");
						append = true;
						return (int)reader["ID"];
					}
				);

			foreach (var item in ids) {
				Console.WriteLine(item);
			}
		}
	}
}
