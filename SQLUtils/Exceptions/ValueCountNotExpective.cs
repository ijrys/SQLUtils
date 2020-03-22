using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils.Exceptions {
	class ValueCountNotExpective:Exception {
		public ValueCountNotExpective() : this("Values count is not equals columns count") { }
		public ValueCountNotExpective(string message) : base(message) { }
	}
}
