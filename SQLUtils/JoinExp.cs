using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils {
	public enum JoinType {
		InnerJoin,
		LeftJoin,
		RightJoin,
		OuterJoin,
		CrossJoin,
	}
	public class JoinExp<T> where T : Command {
		public JoinType Type { get; set; }
		public DataEntity<T> Table { get; set; }
		public string OnExp { get; set; }
		public T Command { get; private set; }


		public JoinExp<T> Join(string table) {
			Table = new DataEntity<T>(Command).EntityName(table);
			return this;
		}
		public JoinExp<T> Join(string table, string asname) {
			Table = new DataEntity<T>(Command);
			Table.EntityName(table).AS(asname);
			return this;
		}
		public JoinExp<T> Join(DataEntity<T> table) {
			Table = table;
			return this;
		}
		public JoinExp<T> Join(SelectCommand selectedtable, string asname) {
			Table = new DataEntity<T>(Command);
			Table.SelectExpression(selectedtable.CommandScript()).AS(asname);
			return this;
		}

		public string CommandScript () {
			string joinfrom = Table.CommandScript();
			if (string.IsNullOrEmpty(joinfrom)) {
				return string.Empty;
			}

			string script;
			switch (Type) {
				case JoinType.InnerJoin:
					script = "inner join ";
					break;
				case JoinType.LeftJoin:
					script = "left join ";
					break;
				case JoinType.RightJoin:
					script = "right join ";
					break;
				case JoinType.OuterJoin:
					script = "full outer join ";
					break;
				case JoinType.CrossJoin:
					script = "cross join";
					break;
				default:
					return string.Empty;
			}
			script += joinfrom;

			if (Type != JoinType.CrossJoin) {
				script += " on " + OnExp;
			}

			return script;
		}

		public T On(string on) {
			OnExp = on;
			return Command;
		}

		public JoinExp(T command) {
			Command = command;
		}

	}
}
