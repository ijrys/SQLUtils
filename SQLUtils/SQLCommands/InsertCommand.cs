using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MiRaI.SQLUtils {
	public class InsertCommand : TableCommand<InsertCommand> {
		private NonQueryExecuter Executer { get; set; }

		public string[] ColumnList { get; private set; }
		public object[] Line { get; private set; }
		public InsertCommand Columns (params string[] columns) {
			ColumnList = columns;
			return this;
		}

		public InsertCommand Value (params object[] values) {
			Line = values;
			return this;
		}

		public NonQueryExecuter Execute() {
			if (Executer == null) {
				Executer = new NonQueryExecuter(ConnectionString, CommandScript(), Parameters);
			}
			return Executer;
		}

		public override string CommandScript() {
			if (ColumnList == null || ColumnList.Length == 0 || Line == null) {
				return string.Empty;
			}
			if (ColumnList.Length != Line.Length) {
				throw new Exceptions.ValueCountNotExpective();
			}

			string [] _argnames = new string[ColumnList.Length];

			string exp = $"insert into {Table.CommandScript()} ({Utils.SafetyEntityName(ColumnList[0])}";
			string vexp = "@arg0";
			_argnames[0] = vexp;

			for (int i = 1; i < ColumnList.Length; i++) {
				exp += ", " + Utils.SafetyEntityName(ColumnList[i]);
				string aname = $"@arg{i}";
				vexp += ", " + aname;
				_argnames[i] = aname;
			}
			exp += $") values ({vexp})";

			Parameters.Clear();
			for (int i = 0; i < ColumnList.Length; i++) {
				Parameters[_argnames[i]] = Line[i];
			}

			return exp;
		}
		public InsertCommand(string connStr) : base(connStr) {
		}
	}
}
