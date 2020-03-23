using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils {
	public class ProcessCommand : TableCommand<ProcessCommand> {
		private NormalQueryExecuter Executer { get; set; }

		public NormalQueryExecuter Execute (params object[] args) {
			string cmdTest = CommandScript();

			for (int i = 0; i < args.Length; i++) {
				if (i == 0) {
					cmdTest += " ";
				}
				else {
					cmdTest += ", ";
				}
				string argname = $"@arg{i}";
				cmdTest += argname;
				AddParameter(argname, args[i]);
			}

			if (Executer == null) {
				Executer = new NormalQueryExecuter(ConnectionString, cmdTest, Parameters);
			}
			return Executer;
		}

		public override string CommandScript() {
			string proname = Table.CommandScript();
			string cmdtxt = "EXECUTE " + proname;
			return cmdtxt;
		}


		public ProcessCommand(string connStr) : base(connStr) {
		}
	}
}
