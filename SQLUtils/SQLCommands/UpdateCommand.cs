using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils {
	public class UpdateCommand : TableCommand<UpdateCommand> {
		public List<string> SetExps { get; private set; }
		public NonQueryExecuter Executer { get; private set; }

		public string WhereExp { get; private set; }
		public bool SafetyCheck { get; set; }

		public UpdateCommand SetSafetyCheck(bool safetyCheck) {
			SafetyCheck = safetyCheck;
			return this;
		}
		public UpdateCommand Where(string whereExp) {
			WhereExp = whereExp;
			return this;
		}
		public UpdateCommand AppendSet(string colname, object value) {
			string argname = $"@arg{SetExps.Count}";
			colname = Utils.SafetyEntityName(colname);
			SetExps.Add($"{colname} = {argname}");
			Parameters.Add(argname, value);
			return this;
		}
		public UpdateCommand AppendSet(string exp) {
			SetExps.Add(exp);
			return this;
		}

		public NonQueryExecuter Execute () {
			if (Executer == null) {
				Executer = new NonQueryExecuter(ConnectionString, CommandScript(), Parameters);
			}
			return Executer;
		} 

		public override string CommandScript() {
			if (SafetyCheck && string.IsNullOrEmpty(WhereExp)) {
				throw new Exceptions.UnSafeExpressionException("need where expression");
			}

			if (Table == null || SetExps.Count == 0) {
				return string.Empty;
			}
			string fromExp = Table.CommandScript();
			if (string.IsNullOrEmpty(fromExp)) {
				return string.Empty;
			}

			string exp = $"update {Table.CommandScript()} set";
			exp += " " + SetExps[0];
			for (int i = 1; i < SetExps.Count; i++) {
				exp += ", " + SetExps[i];
			}

			if (!string.IsNullOrEmpty(WhereExp)) {
				exp += " where " + WhereExp;
			}

			return exp;
		}

		public UpdateCommand(string connStr) : base(connStr) {
			SetExps = new List<string>();
		}
	}
}
