using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace VKeCRM.Framework.Mvc
{
	public class AsyncMvcRouteHandler : IRouteHandler
	{
		public PresentationType PresentationType
		{
			get;
			private set;
		}

		public AsyncMvcRouteHandler() : this(PresentationType.NoneDefined) { }

		public AsyncMvcRouteHandler(PresentationType pType)
		{
			PresentationType = pType;
		}

		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			string controllerName = requestContext.RouteData.GetRequiredString("controller");

			if (PresentationType == PresentationType.Tapi)
			{
				string version = requestContext.RouteData.GetRequiredString("version");

				if (string.IsNullOrEmpty(version))
					throw new ArgumentNullException("version");

				double verAsDouble = 0;
				if (!double.TryParse(version, out verAsDouble))
				{
					throw new ArgumentException(string.Format("Invalid version of {0}.", version));
				}
				else
				{
					controllerName = string.Format("v{0}_{1}", version.Replace(".", string.Empty), controllerName);
				}
			}

			var factory = ControllerBuilder.Current.GetControllerFactory();
			var controller = factory.CreateController(requestContext, controllerName);
			if (controller == null)
			{
				throw new InvalidOperationException(
					String.Format(
						"The IControllerFactory '{0}' did not return a controller for a controller named '{1}'.",
						factory.GetType(),
						controllerName));
			}

			var coreController = controller as Controller;
			if (coreController == null)
			{
				return new SyncMvcHandler(controller, factory, requestContext);
			}
			else
			{
				string actionName = requestContext.RouteData.GetRequiredString("action");
				return IsAsyncAction(coreController, actionName, requestContext) ?
					(IHttpHandler)new AsyncMvcHandler(coreController, factory, requestContext) :
					(IHttpHandler)new SyncMvcHandler(controller, factory, requestContext);
			}
		}

		private static object s_methodInvokerMutex = new object();
		private static MethodInvoker s_controllerDescriptorGetter;

		internal static bool IsAsyncAction(
			Controller controller, string actionName, RequestContext requestContext)
		{
			var actionInvoker = controller.ActionInvoker as ControllerActionInvoker;
			if (actionInvoker == null) return false;

			if (s_controllerDescriptorGetter == null)
			{
				lock (s_methodInvokerMutex)
				{
					if (s_controllerDescriptorGetter == null)
					{
						BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
						MethodInfo method = typeof(ControllerActionInvoker).GetMethod(
							"GetControllerDescriptor", bindingFlags);
						s_controllerDescriptorGetter = new MethodInvoker(method);
					}
				}
			}

			var controllerContext = new ControllerContext(requestContext, controller);
			var controllerDescriptor = (ControllerDescriptor)s_controllerDescriptorGetter.Invoke(
				actionInvoker, controllerContext);

			var actionDescriptor = controllerDescriptor.FindAction(controllerContext, actionName);

			if (actionDescriptor != null && actionDescriptor.GetCustomAttributes(typeof(AsyncActionAttribute), false).Any())
				return true;
			else
			{
				actionDescriptor = controllerDescriptor.FindAction(controllerContext, string.Concat(actionName, "Completed"));
			}

			return actionDescriptor == null ? false :
			  actionDescriptor.GetCustomAttributes(typeof(AsyncActionAttribute), false).Any();
		}
	}

	public enum PresentationType
	{
		NoneDefined = 0,
		Portal = 1,
		Trading = 2,
		Api = 3,
		Apply = 4,
		Tapi = 5
	}
}
