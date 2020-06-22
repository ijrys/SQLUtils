using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils {
	public class ScriptCommand : Command<ScriptCommand> {
		public string SQLScript { get; set; }
		public NormalQueryExecuter Executer { get; private set; }

		public ScriptCommand Script(string script) {
			SQLScript = script;
			return this;
		}

		public override string CommandScript() {
			return SQLScript;
		}

		public NormalQueryExecuter Execute() {
			if (Executer == null) {
				Executer = new NormalQueryExecuter(ConnectionString, CommandScript(), Parameters);
			}
			return Executer;
		}
		public ScriptCommand(string connStr) : base(connStr) {
		}
	}
}
