using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Mvc.Exceptions
{
	public class JsonDeserializeError : MvcExceptionBase
	{
		public JsonDeserializeError(string errorMessage)
			: this(errorMessage, null)
		{
		}

		public JsonDeserializeError(string errorMessage, Exception innerException)
			: base(MvcErrorType.JsonDeserializeError, errorMessage, innerException)
		{
		}
	}
}
