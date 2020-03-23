using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils {
	class Utils {
		public static string SafetyEntityName (string name) {
			name = name.Trim();
			if (!name.StartsWith("[")) {
				name = '[' + name + ']';
			}
			return name;
		}
		public static string SafetyName (string tableExp) {
			tableExp = tableExp.Trim();
			if (tableExp.StartsWith("(")) { // select expression
				return tableExp; 
			}
			
			return SafetyEntityName(tableExp);
		}
		public static string MergeSchemaAndTable(string schema, string table) {
			string fname;
			schema = schema.Trim();
			table = table.Trim();
			if (!string.IsNullOrEmpty(schema)) {
				fname = SafetyEntityName(schema) + ".";
			} else {
				fname = "";
			}
			fname += SafetyEntityName(table);

			return fname;
		}
	}
}
