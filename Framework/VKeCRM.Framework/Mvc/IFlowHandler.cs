using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace VKeCRM.Framework.Mvc
{
	interface IFlowHandler
	{
		void BeforeActionExecuting(ControllerContext context);

		void AfterActionExecuted(ActionExecutedContext context);
	}
}
