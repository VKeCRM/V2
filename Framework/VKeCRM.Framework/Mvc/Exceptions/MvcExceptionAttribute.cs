using System;
using log4net;
using System.Web.Mvc;

namespace VKeCRM.Framework.Mvc.Exceptions
{
	public class MvcExceptionAttribute : Attribute, IExceptionFilter
	{
		private static readonly ILog Logger;

		static MvcExceptionAttribute()
		{
			Logger = LogManager.GetLogger("logError");
		}

		public void OnException(ExceptionContext filterContext)
		{
			Logger.Error(filterContext.Controller.GetType().FullName, filterContext.Exception);
		}
	}
}
