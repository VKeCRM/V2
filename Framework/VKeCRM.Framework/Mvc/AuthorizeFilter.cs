using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace VKeCRM.Framework.Mvc
{
	public class AuthorizeFilter : System.Web.Mvc.AuthorizeAttribute
	{
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			if (httpContext.Session != null)
			{
				var authorised = httpContext.Session["Authorised"];
				if (!(authorised is bool) || !(bool) authorised)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			return base.AuthorizeCore(httpContext);
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
			base.HandleUnauthorizedRequest(filterContext);
		}
	}
}
