using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Mvc.Exceptions
{
	public class MvcKickOutException : MvcExceptionBase
	{
		public string ReasonCode
		{
			get;
			set;
		}

		public MvcKickOutException(string errorMessage, string reasonCode)
			: base(MvcErrorType.SessionKickOutException, errorMessage)
		{
			this.ReasonCode = reasonCode;
		}

		public override JsonResult ToJsonResult()
		{
			JsonResult jr = new JsonResult();

			jr.Error = (int)MvcErrorType;
			jr.DataSource = ReasonCode;
			jr.ErrorMessage = this.Message;

			return jr;
		}
	}
}
