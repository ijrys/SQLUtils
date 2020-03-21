using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MiRaI.SQLUtils;

namespace Test {
	class Program {
		static void Main(string[] args) {
			//string script = new Connection("")
			//	.Select()
			//	.From("TableA", "a")
			//	.Inner.Join("TableB", "b").On("b.PID = a.ID")
			//	.Where("a.ID = @id")
			//	.AddParameter("id", 1)
			//	.CommandScript();

			//SelectCommand command = new Connection(null)                   //"server=.\\SQLEXPRESS;database=MiRaIUser;User Id=miraiadmin;Password=123456"
			//	.Select()
			//	.From("User", "a")
			//	.Outer.Join(
			//		new SelectCommand()
			//		.Select()
			//		.From("UserTmpKey")
			//		.Where("[UID] < 5"), "b")
			//	.On("a.Id = b.UID")
			//	.Where("a.[Id] = @id")
			//	.AddParameter("id", 1);


			//string script = command.CommandScript();
			//Console.WriteLine(script);

			//Dictionary<string, object> values = command.Execute().FirstLine((SqlDataReader reader) => {
			//	Dictionary<string, object> values = new Dictionary<string, object>();
			//	for (int i = 0; i < reader.FieldCount; i++) {
			//		values[reader.GetName(i)] = reader.GetValue(i);
			//	}
			//	return values;
			//}) as Dictionary<string, object>;

			//foreach (var item in values) {
			//	Console.WriteLine($"{item.Key}    \t | {item.Value}");
			//}

			Dictionary<string, object> values = new Connection(null)
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
				.Execute().SetConnection(new SqlConnection("server=.\\SQLEXPRESS;database=MiRaIUser;User Id=miraiadmin;Password=123456"))
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
