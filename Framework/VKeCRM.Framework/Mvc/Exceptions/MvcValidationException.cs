using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Mvc.Exceptions
{
	public class MvcValidationException : MvcExceptionBase
	{
		private List<PropertyModel> ErrorProperties
		{
			get;
			set;
		}

		public MvcValidationException(List<PropertyModel> errorProperties)
			: base(MvcErrorType.ValidationFailedAtServer, null)
		{
			this.ErrorProperties = new List<PropertyModel>(errorProperties);
		}

		public override JsonResult ToJsonResult()
		{
			JsonResult jr = new JsonResult();

			jr.Error = (int)MvcErrorType;
			jr.DataSource = ErrorProperties.ToArray();

			return jr;
		}
	}
}
