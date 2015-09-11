using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace VKeCRM.Framework.Mvc
{
	public sealed class AsyncCallbackModelBinder : IModelBinder
	{
		public object BindModel(
			ControllerContext controllerContext,
			ModelBindingContext bindingContext)
		{
			return ((VKeCRM.Framework.Mvc.ControllerBase)controllerContext.Controller).GetAsyncCallback();
		}
	}
}
