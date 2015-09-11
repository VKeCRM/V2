using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Mvc.Exceptions
{
	public class MvcAuthorizeException : MvcExceptionBase
	{
		public MvcAuthorizeException(string errorMessage)
			: base(MvcErrorType.AuthenticationFailed, errorMessage)
		{
		}
	}
}
