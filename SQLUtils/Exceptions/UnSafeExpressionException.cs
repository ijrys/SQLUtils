using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils.Exceptions {
	public class UnSafeExpressionException :Exception {
		public UnSafeExpressionException () :this ("UnSafe Expression") { }
		public UnSafeExpressionException(string message) : base(message) { }
	}
}
