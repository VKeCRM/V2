using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using System.Threading;


namespace VKeCRM.Framework.Mvc
{
	public class AsyncMvcHandler : IHttpAsyncHandler, IRequiresSessionState
	{
		public AsyncMvcHandler(
			Controller controller,
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

		public Controller Controller { get; private set; }

		public RequestContext RequestContext { get; private set; }

		public IControllerFactory ControllerFactory { get; private set; }

		public HttpContext Context { get; private set; }

		#region IHttpHandler Members

		public bool IsReusable
		{
			get { return false; }
		}

		public void ProcessRequest(HttpContext context)
		{
			throw new NotSupportedException();
		}

		#endregion

		#region IHttpAsyncHandler Members

		public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
		{
			this.Context = context;
			((VKeCRM.Framework.Mvc.ControllerBase)this.Controller).SetAsyncCallback(cb).SetAsyncState(extraData);

			try
			{
				//System.Diagnostics.Debug.WriteLine("Begin"+ Thread.CurrentThread.ManagedThreadId); 

				(this.Controller as IController).Execute(this.RequestContext);

				VKeCRM.Framework.Mvc.JsonResult asyncResult = ((VKeCRM.Framework.Mvc.ControllerBase)this.Controller).GetAsyncResult();
								
				return new AsynchOperation(extraData); ;
			}
			catch
			{
				this.ControllerFactory.ReleaseController(this.Controller);
				throw;
			}
		}

		public void EndProcessRequest(IAsyncResult result)
		{
			try
			{
				HttpContext.Current = this.Context;
				//System.Diagnostics.Debug.WriteLine("End" + Thread.CurrentThread.ManagedThreadId);
				AsyncCallback cb = ((VKeCRM.Framework.Mvc.ControllerBase)this.Controller).GetAsyncCallback();
				object extraData = ((VKeCRM.Framework.Mvc.ControllerBase)this.Controller).GetAsyncState();
				if (result == null)
				{
					result= new AsynchOperation(extraData);
				}
				cb(result);
			}
			finally
			{
				this.ControllerFactory.ReleaseController(this.Controller);
			}
		}

		class AsynchOperation : IAsyncResult
		{
			 public AsynchOperation(object asyncState)
            {
                this.AsyncState = asyncState;
            }

            public object AsyncState { get; private set; }

            private ManualResetEvent m_event;
            public WaitHandle AsyncWaitHandle
            {
                get
                {
                    if (this.m_event == null)
                    {
                        this.m_event = new ManualResetEvent(true);
                    }

                    return this.m_event;
                }
            }

            public bool CompletedSynchronously { get { return true; } }

            public bool IsCompleted { get { return true; } }        
		}

		#endregion		

	}
}
