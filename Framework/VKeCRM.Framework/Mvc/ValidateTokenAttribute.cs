using System;
using System.Web;
using System.Web.Mvc;
using VKeCRM.Common.Logging;

namespace VKeCRM.Framework.Mvc
{
	public class ValidateTokenAttribute : AuthorizeAttribute
	{
		public override void  OnAuthorization(AuthorizationContext filterContext)
		{
			//Get token from request
			if (HttpContext.Current.Request.ServerVariables["Request_Method"].Equals("POST"))
			{
				 var tokenFromParamter = HttpContext.Current.Request.Form[CsrfToken.TokenName];

				 if (!(HttpContext.Current.Request.Cookies[CsrfToken.TokenName] != null && tokenFromParamter != null
						  && HttpContext.Current.Request.Cookies[CsrfToken.TokenName].Value == tokenFromParamter))
				{
					filterContext.Result = new JsonResult(MvcErrorType.TokenValidationgFailed, "Token validation failed.");
					
					 LoggerManager.PortalWebLogger.Error(string.Format("CSRF token validation failed. Cookie: {0}		Form: {1}",
							HttpContext.Current.Request.Cookies[CsrfToken.TokenName].Value, tokenFromParamter));


				}

			}
			//else
			//{
			//    tokenFromParamter = HttpContext.Current.Request.QueryString[CsrfTokenName];	
			//}

			//Re-assign the token.
			//CsrfToken.SetNewCsrfCookie(filterContext.HttpContext.Response.Cookies);
		}
	}
}
