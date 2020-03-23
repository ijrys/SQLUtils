using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils {
	/// <summary>
	/// a entity contents datas, like table and exectued process
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DataEntity<T> where T:Command {
		public string TableExpression { get; private set; }
		public string ShortName { get; private set; }
		private T RebackObject { get; set; }

		public DataEntity<T> EntityName(string name) {
			if (string.IsNullOrWhiteSpace(name)) {
				TableExpression = null;
			}
			else {
				TableExpression = Utils.SafetyEntityName(name);
			}
			return this;
		}
		public DataEntity<T> EntityName(string schema, string name) {
			string exp = Utils.MergeSchemaAndTable(schema, name);
			TableExpression = exp;
			return this;
		}
		public DataEntity<T> SelectExpression(string expression) {
			if (string.IsNullOrWhiteSpace(expression)) {
				TableExpression = null;
			}
			else {
				if (!expression.StartsWith("(")) {
					expression = '(' + expression + ')';
				}
				TableExpression = expression;
			}
			return this;
		}

		public T AS () {
			ShortName = string.Empty;
			return RebackObject;
		}
		public T AS(string asname) {
			if (string.IsNullOrWhiteSpace(asname)) {
				asname = null;
			}
			ShortName = Utils.SafetyEntityName(asname);
			return RebackObject;
		}

		public string CommandScript() {
			if (string.IsNullOrEmpty(TableExpression)) {
				return string.Empty;
			}
			string re = TableExpression;
			if (!string.IsNullOrEmpty(ShortName)) {
				re += " as " + ShortName;
			}
			return re;
		}

		public DataEntity(T rebackObj) {
			RebackObject = rebackObj;
		}
	}
}
