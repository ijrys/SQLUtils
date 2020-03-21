using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils {
	public abstract class Command {
		public string ConnectionString { get; private set; }

		public abstract string CommandScript();

		internal Command(string connStr) {
			ConnectionString = connStr;

		}
	}

	public abstract class Command<T> : Command where T : Command {
		public Dictionary<string, object> Parameters { get; private set; }
		public T AddParameter(string name, object value) {
			Parameters[name] = value;
			return this as T;
		}

		internal Command(string connStr) : base(connStr) {
			Parameters = new Dictionary<string, object>();
		}
	}
}
