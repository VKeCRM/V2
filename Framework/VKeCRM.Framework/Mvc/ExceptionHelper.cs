using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Mvc
{
	public static class ExceptionHelper
	{
		public static void AssertParamNotNull(object value, string paramName)
		{
			if (value == null) throw new ArgumentNullException(paramName);
		}
	}
}
