using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils {


	class DeleteCommand : Command<DeleteCommand> {
		private NonQueryExecuter Executer { get; set; }

		public Table Table { get; private set; }
		public string WhereExp { get; private set; }
		public bool SafetyCheck { get; set; }

		public DeleteCommand From(string table) {
			Table = new Table().TableName(table);
			return this;
		}
		public DeleteCommand From(Table table) {
			Table = table;
			return this;
		}

		public DeleteCommand SetSafetyCheck(bool safetyCheck) {
			SafetyCheck = safetyCheck;
			return this;
		}

		public override string CommandScript() {
			if (SafetyCheck && string.IsNullOrEmpty(WhereExp)) {
				throw new Exceptions.UnSafeExpressionException("need where expression");
			}

			if (Table == null) {
				return string.Empty;
			}
			string fromExp = Table.CommandScript();
			if (string.IsNullOrEmpty(fromExp)) {
				return string.Empty;
			}

			string script = $"Delete {fromExp}";

			if (!string.IsNullOrEmpty(WhereExp)) {
				script += $" where {WhereExp}";
			}

			return script;
		}

		public NonQueryExecuter Execute() {
			if (Executer == null) {
				Executer = new NonQueryExecuter(ConnectionString, CommandScript(), Parameters);
			}
			return Executer;
		}

		public DeleteCommand(string connStr) : base(connStr) {
		}
	}
}
