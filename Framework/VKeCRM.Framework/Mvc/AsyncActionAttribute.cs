using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Runtime.InteropServices;
using System.Threading;

namespace VKeCRM.Framework.Mvc
{

	public sealed class AsyncActionAttribute : ActionFilterAttribute
	{	
		// Summary:
		//     Called after the action method executes.
		//
		// Parameters:
		//   filterContext:
		//     The filter context.
		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{ 			
			VKeCRM.Framework.Mvc.JsonResult view = (VKeCRM.Framework.Mvc.JsonResult)(filterContext.Result);
			((VKeCRM.Framework.Mvc.ControllerBase)filterContext.Controller).SetAsyncResult(view);			
		}	
	}
}
