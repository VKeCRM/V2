using System;

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using System.Web;
using System.Web.Mvc;

namespace VKeCRM.Framework.Mvc
{
	public class ErrorHandlerAttribute : HandleErrorAttribute
	{
		public override void OnException(ExceptionContext filterContext)
		{
			IErrorHanlder errorHanlder = filterContext.Controller as IErrorHanlder;

			if (errorHanlder == null)
			{
				throw new ArgumentException(ZECCO_AUTHORIZAT_TYPE_ERROR);
			}

			//The flag tell us that the exception has been handled
			if (filterContext.ExceptionHandled)
			{
				return;
			}

			//Here we return the VKeCRMJsonResult which contain the error flag and error message.
			//filterContext.Result = errorHanlder.HandleError(filterContext.Exception);
			
			filterContext.Result = errorHanlder.HandleError(filterContext.Exception);

			filterContext.ExceptionHandled = true;
		}

		private const string ZECCO_AUTHORIZAT_TYPE_ERROR = @"Controller which used the VKeCRMErrorHandlerAttribute didn't implement IVKeCRMAuthorization Interface";
	}
}
