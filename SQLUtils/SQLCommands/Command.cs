using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils {
	public abstract class Command {
		public string ConnectionString { get; private set; }

		public abstract string CommandScript();

		public Command(string connStr) {
			ConnectionString = connStr;
		}
	}

	public abstract class Command<T> : Command where T : Command {
		public Dictionary<string, object> Parameters { get; private set; }
		public T AddParameter(string name, object value) {
			Parameters[name] = value;
			return this as T;
		}

		public Command(string connStr) : base(connStr) {
			Parameters = new Dictionary<string, object>();
		}
	}

	public abstract class TableCommand<T> : Command<T> where T : Command {
		public DataEntity<T> Table { get; protected set; }

		public T From(string table) {
			Table = new DataEntity<T>(this as T).EntityName(table);
			return this as T;
		}
		public T From(string schema, string table) {
			Table = new DataEntity<T>(this as T);
			Table.EntityName(schema, table).AS();
			return this as T;
		}

		public T FromAs(string table, string asname) {
			Table = new DataEntity<T>(this as T);
			return Table.EntityName(table).AS(asname);
		}
		public T FromAs(string schema, string table, string asname) {
			Table = new DataEntity<T>(this as T);
			return Table.EntityName(schema, table).AS(asname);
		}

		public T From(DataEntity<T> table) {
			Table = table;
			return this as T;
		}

		public TableCommand(string connStr) : base(connStr) {
		}
	}
}
