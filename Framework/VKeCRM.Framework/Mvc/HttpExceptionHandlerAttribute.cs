using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace VKeCRM.Framework.Mvc
{
	public class HttpExceptionHandlerAttribute : HandleErrorAttribute
	{
		public override void OnException(ExceptionContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}

			// If custom errors are disabled, we need to let the normal ASP.NET exception handler
			// execute so that the user can see useful debugging information.
			if (filterContext.ExceptionHandled)
			{
				return;
			}

			Exception exception = filterContext.Exception;

			// If this is not an HTTP 500 (for example, if somebody throws an HTTP 404 from an action method),
			// ignore it.
			if (new HttpException(null, exception).GetHttpCode() != 500)
			{
				return;
			}

			if (!ExceptionType.IsInstanceOfType(exception))
			{
				return;
			}

			//filterContext.ExceptionHandled = true;
			//filterContext.HttpContext.Response.Clear();
			//filterContext.HttpContext.Response.StatusCode = 500;

			filterContext.HttpContext.Response.Redirect("/genericerrorpage.aspx");

			// Certain versions of IIS will sometimes use their own error page when
			// they detect a server error. Setting this property indicates that we
			// want it to try to render ASP.NET MVC's error page instead.
			filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
		}
	}
}
