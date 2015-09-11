using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace VKeCRM.Framework.Mvc
{
	public static class ControllerExtensions
	{
		static ControllerExtensions()
		{
			ASYNC_CALLBACK_KEY = Guid.NewGuid().ToString();
			ASYNC_STATE_KEY = Guid.NewGuid().ToString();
			ASYNC_RESULT_KEY = Guid.NewGuid();			
		}

		private static readonly string ASYNC_CALLBACK_KEY;
		private static readonly string ASYNC_STATE_KEY;
		private static readonly Guid ASYNC_RESULT_KEY;

		internal static ControllerBase SetAsyncCallback(this ControllerBase controller, AsyncCallback callback)
		{
			ExceptionHelper.AssertParamNotNull(callback, "callback");

			CallContext.SetData(ASYNC_CALLBACK_KEY, callback);
			return controller;
		}

		internal static ControllerBase SetAsyncState(this ControllerBase controller, object asyncState)
		{
			CallContext.SetData(ASYNC_STATE_KEY, asyncState);
			return controller;
		}
		public static object GetAsyncState(this ControllerBase controller)
		{
			return CallContext.GetData(ASYNC_STATE_KEY);
		}
		public static AsyncCallback GetAsyncCallback(this ControllerBase controller)
		{
			return (AsyncCallback)CallContext.GetData(ASYNC_CALLBACK_KEY);
		}


		internal static ControllerBase SetAsyncResult(this ControllerBase controller, VKeCRM.Framework.Mvc.JsonResult asyncResult)
		{
			ExceptionHelper.AssertParamNotNull(controller, "controller");
			ExceptionHelper.AssertParamNotNull(asyncResult, "asyncResult");

			controller.ControllerContext.HttpContext.Items[ASYNC_RESULT_KEY] = asyncResult;
			return controller;
		}

		internal static VKeCRM.Framework.Mvc.JsonResult GetAsyncResult(this ControllerBase controller)
		{
			ExceptionHelper.AssertParamNotNull(controller, "controller");
			return (VKeCRM.Framework.Mvc.JsonResult)controller.ControllerContext.HttpContext.Items[ASYNC_RESULT_KEY];
		}		
	}
}
