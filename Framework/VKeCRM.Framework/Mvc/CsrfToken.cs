using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace VKeCRM.Framework.Mvc
{
	public static class CsrfToken
	{
		public static string TokenName
		{
			get { return "VKeCRMToken"; }
		}

		public static HttpCookie SetNewCsrfCookie(HttpCookieCollection cookies)
		{
			return SetNewCsrfCookie(cookies, Guid.NewGuid().ToString());
		}

		public static HttpCookie SetNewCsrfCookie(HttpCookieCollection cookies, string VKeCRMToken)
		{
			HttpCookie cookie = new HttpCookie(TokenName)
			{
				Value = VKeCRMToken,
				Domain = "localhost",
				Secure = true,
			};

			if (cookies[TokenName] == null)
			{
				cookies.Add(cookie);
			}
			else
			{
				cookies.Set(cookie);
			}

			//save token in session
			HttpContext.Current.Session[TokenName] = VKeCRMToken;

			return cookie;
		}
	}
}
