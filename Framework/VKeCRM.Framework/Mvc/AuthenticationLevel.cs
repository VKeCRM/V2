using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Mvc
{
	/// <summary>
	/// Class to determine the authentication type for controller.
	/// </summary>
	public enum AuthenticationLevel
	{
		CookieAuthenticated,
		FullyAuthenticated,
		IPAuthenticated,
		None
	}
}
