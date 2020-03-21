using System;
using System.Collections.Generic;
using System.Text;

namespace MiRaI.SQLUtils {
	class UpdateCommand : TableCommand<UpdateCommand> {

		public override string CommandScript() {
			throw new NotImplementedException();
		}

		public UpdateCommand(string connStr) : base(connStr) {
		}
	}
}
