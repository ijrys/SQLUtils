using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils.Exceptions {
	public class LineCountOutOfRangeException : Exception {
		public LineCountOutOfRangeException():base("data contents more lines than range") {


		}

		public LineCountOutOfRangeException(string message) :base(message) { }
	}
}
