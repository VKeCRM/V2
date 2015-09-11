using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Mvc.Exceptions
{
	public class PermissionDeniedException : MvcExceptionBase
	{
		public PermissionDeniedException(string where2Go)
			: base(MvcErrorType.PermissionDenied, null)
		{
			Where2Go = where2Go;
		}

		public string Where2Go
		{
			get;
			private set;
		}

		public override JsonResult ToJsonResult()
		{
			return new JsonResult
			{
				Error = (int)MvcErrorType,
				DataSource = new { Where2Go = Where2Go },
				ErrorMessage = "Sorry, you have no perission to access this part. Please contact admin for help."
			};
		}
	}
}
