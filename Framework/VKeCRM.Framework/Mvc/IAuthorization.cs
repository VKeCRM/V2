using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace VKeCRM.Framework.Mvc
{
	interface IAuthorization
	{
		AuthenticationLevel RequireAuthenticationLevel { get; }

		void VerifyToken(AuthorizationContext context);

		void DoAuthentication(AuthenticationLevel authenticationLevel);

		void VerifyPermission(AuthorizationContext context);
	}
}