using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Common.NamedPipe
{
	public class ReceivedMessageEventArgs : EventArgs
	{
		public ReceivedMessageEventArgs(string message)
		{
			Message = message;
		}

		public string Message { get; set; }
	}
}
