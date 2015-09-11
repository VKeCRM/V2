using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace VKeCRM.Framework.Mvc
{
	public class HttpExceptionResult : ActionResult
	{
		public int StatusCode
		{
			get; set;
		}

		/// <summary>
		/// StatusCode indicate the http stand error, such as: 404, 500 and so on
		/// </summary>
		/// <param name="statusCode"></param>
		public HttpExceptionResult(int statusCode)
		{
			StatusCode = statusCode;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			context.HttpContext.Response.Clear();
			context.HttpContext.Response.StatusCode = StatusCode;

			// Certain versions of IIS will sometimes use their own error page when
			// they detect a server error. Setting this property indicates that we
			// want it to try to render ASP.NET MVC's error page instead.
			context.HttpContext.Response.TrySkipIisCustomErrors = true;
		}
	}
}
