using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace VKeCRM.Framework.Mvc
{
	public class ActionFlowAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
	
			(filterContext.Controller as IFlowHandler).BeforeActionExecuting(filterContext);
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			base.OnActionExecuted(filterContext);

			(filterContext.Controller as IFlowHandler).AfterActionExecuted(filterContext);
		}
	}
}
