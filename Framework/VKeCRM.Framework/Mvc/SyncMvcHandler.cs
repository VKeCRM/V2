using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.Mvc;

namespace VKeCRM.Framework.Mvc
{
	public class SyncMvcHandler : IHttpHandler, IRequiresSessionState
	{
		public SyncMvcHandler(
		   IController controller,
		   IControllerFactory controllerFactory,
		   RequestContext requestContext)
		{
			ExceptionHelper.AssertParamNotNull(controller, "controller");
			ExceptionHelper.AssertParamNotNull(controllerFactory, "controllerFactory");
			ExceptionHelper.AssertParamNotNull(requestContext, "requestContext");

			this.Controller = controller;
			this.ControllerFactory = controllerFactory;
			this.RequestContext = requestContext;
		}

		public IController Controller { get; private set; }

		public RequestContext RequestContext { get; private set; }

		public IControllerFactory ControllerFactory { get; private set; }

		public virtual bool IsReusable { get { return false; } }

		public virtual void ProcessRequest(HttpContext context)
		{
			try
			{
				this.Controller.Execute(this.RequestContext);
			}
			finally
			{
				this.ControllerFactory.ReleaseController(this.Controller);
			}
		}
	}
}
