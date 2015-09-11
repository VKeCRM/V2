using System;
using System.Web.Mvc;
using VKeCRM.Framework.Mvc.Exceptions;
using System.Reflection;
using System.Linq;

namespace VKeCRM.Framework.Mvc
{
	public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
	{
		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			IAuthorization authorization = (filterContext.Controller as IAuthorization);

			// Verify Token  -- remove this step - do not verify the authorization
			//authorization.VerifyToken(filterContext);

			// Verify Authentication
			AuthenticationLevel controllerLevelAuthentication = authorization.RequireAuthenticationLevel;

			// Has method level Authentication?
			object customAttribute = null;

			if (filterContext.ActionDescriptor != null)
			{
				object[] customeAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthLevelAttribute), true);

				customAttribute = customeAttributes != null && customeAttributes.Length > 0 ? customeAttributes[0] : null;
			}

			if (null != customAttribute)
			{
				controllerLevelAuthentication = ((AuthLevelAttribute)customAttribute).AuthenticationLevel;
			}

			authorization.DoAuthentication(controllerLevelAuthentication);

			if (controllerLevelAuthentication == AuthenticationLevel.CookieAuthenticated ||
				controllerLevelAuthentication == AuthenticationLevel.FullyAuthenticated)
			{
				authorization.VerifyPermission(filterContext);
			}
		}
	}
}
