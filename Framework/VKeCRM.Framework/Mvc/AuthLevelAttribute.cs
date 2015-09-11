using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Mvc
{
	[AttributeUsage(AttributeTargets.Method)]
	public class AuthLevelAttribute : Attribute
	{
		public AuthLevelAttribute(AuthenticationLevel authenticationLevel)
		{
			AuthenticationLevel = authenticationLevel;
		}

		public AuthenticationLevel AuthenticationLevel
		{
			get;
			private set;
		}
	}
}
