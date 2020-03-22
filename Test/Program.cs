using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MiRaI.SQLUtils;

namespace Test {
	class Program {
		static void Main(string[] args) {
			SQL.DefaultConnectionString = "server=.\\SQLEXPRESS;database=MiRaIUser;User Id=miraiadmin;Password=123456";

			UpdateTest();


		}

		static void UpdateTest () {
			SelectTest();

			int re = SQL.Connection()
				.Update().From("User")
				.AppendSet("nickname", "sqlutils")
				.Where("[id] = @id")
				.AddParameter("id", 1)
				.Execute()
				.NonQuery();
			Console.WriteLine($"{re} row(s) changed");

			SelectTest();
		}

		static void SelectTest () {
			Dictionary<string, object> values = SQL.Connection()
				.Select()
				.From("User", "a")
				.Outer.Join(
					new SelectCommand()
					.Select()
					.From("UserTmpKey")
					.Where("[UID] < 5"), "b")
				.On("a.Id = b.UID")
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
	}
}
