using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils {
	public class SelectCommand : Command<SelectCommand> {
		private FullQueryExecuter Executer { get; set; }
		public string Columns { get; private set; }
		public Table Table { get; private set; }
		public string WhereExp { get; private set; }
		public List<JoinExp<SelectCommand>> JoinExps { get; private set; }

		public override string CommandScript() {
			if (Table == null) {
				return string.Empty;
			}
			string fromExp = Table.CommandScript();
			if (string.IsNullOrEmpty(fromExp)) {
				return string.Empty;
			}
			string columns = Columns;
			if (string.IsNullOrEmpty(columns)) {
				columns = "*";
			}

			string script = $"select {columns} from {fromExp}";
			
			string joinscript = string.Empty;
			foreach (JoinExp<SelectCommand> item in JoinExps) {
				string tscript = item.CommandScript();
				if (!string.IsNullOrEmpty(tscript)) {
					joinscript += " " + item.CommandScript();
				}
			}
			script += joinscript;

			if (!string.IsNullOrEmpty(WhereExp)) {
				script += " where " + WhereExp;
			}

			return script;
		}

		public SelectCommand Where(string whereExp) {
			WhereExp = whereExp;
			return this;
		}

		public SelectCommand Select(string columns) {
			Columns = columns;
			return this;
		}
		public SelectCommand Select() {
			Columns = null;
			return this;
		}

		public SelectCommand From(string table) {
			Table = new Table().TableName(table);
			return this;
		}
		public SelectCommand From(string table, string asname) {
			Table = new Table().TableName(table).AS(asname);
			return this;
		}
		public SelectCommand From(Table table) {
			Table = table;
			return this;
		}

		public JoinExp<SelectCommand> Inner {
			get {
				JoinExp<SelectCommand> join = new JoinExp<SelectCommand>(this);
				join.Type = JoinType.InnerJoin;
				JoinExps.Add(join);
				return join;
			}
		}
		public JoinExp<SelectCommand> Left {
			get {
				JoinExp<SelectCommand> join = new JoinExp<SelectCommand>(this);
				join.Type = JoinType.LeftJoin;
				JoinExps.Add(join);
				return join;
			}
		}
		public JoinExp<SelectCommand> Right {
			get {
				JoinExp<SelectCommand> join = new JoinExp<SelectCommand>(this);
				join.Type = JoinType.RightJoin;
				JoinExps.Add(join);
				return join;
			}
		}
		public JoinExp<SelectCommand> Outer {
			get {
				JoinExp<SelectCommand> join = new JoinExp<SelectCommand>(this);
				join.Type = JoinType.OuterJoin;
				JoinExps.Add(join);
				return join;
			}
		}


		public FullQueryExecuter Execute() {
			if (Executer == null) {
				Executer = new FullQueryExecuter(ConnectionString, CommandScript(), Parameters);
			}
			return Executer;
		}


		public SelectCommand() : this(null) {
			Columns = null;
		}

		public SelectCommand(string connStr) : base(connStr) {
			Columns = null;
			JoinExps = new List<JoinExp<SelectCommand>>();
		}
	}
}
