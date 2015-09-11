using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Mvc.Exceptions
{
	public abstract class MvcExceptionBase : Exception
	{
		public MvcErrorType MvcErrorType
		{
			get;
			set;
		}

		public MvcExceptionBase()
			: this(MvcErrorType.None, null)
		{
		}

		public MvcExceptionBase(MvcErrorType errorType, string errorMessage)
			: this(errorType, errorMessage, null)
		{
		}

		public MvcExceptionBase(MvcErrorType errorType, string errorMessage, Exception innerException)
			: base(errorMessage, innerException)
		{
			MvcErrorType = errorType;
		}

		public virtual JsonResult ToJsonResult()
		{
			return new JsonResult
			{
				Error = (int)MvcErrorType,
				ErrorMessage = Message
			};
		}
	}
}
