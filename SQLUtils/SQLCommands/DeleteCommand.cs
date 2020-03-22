using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils {
	public class DeleteCommand : TableCommand<DeleteCommand> {
		private NonQueryExecuter Executer { get; set; }

		public string WhereExp { get; private set; }
		public bool SafetyCheck { get; set; }

		public DeleteCommand SetSafetyCheck(bool safetyCheck) {
			SafetyCheck = safetyCheck;
			return this;
		}

		public DeleteCommand Where(string whereExp) {
			WhereExp = whereExp;
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
			SafetyCheck = true;
		}
	}
}
