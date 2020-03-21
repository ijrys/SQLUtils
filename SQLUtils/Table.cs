using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils {
	public class Table {
		public string TableExpression { get; private set; }
		public string ShortName { get; private set; }
		public Table TableName(string name) {
			if (string.IsNullOrWhiteSpace(name)) {
				TableExpression = null;
			}
			else {
				TableExpression = Utils.SafetyEntityName(name);
			}
			return this;
		}
		public Table SelectExpression(string expression) {
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
		public Table AS(string asname) {
			if (string.IsNullOrWhiteSpace(asname)) {
				asname = null;
			}
			ShortName = Utils.SafetyEntityName(asname);
			return this;
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
	}
}
