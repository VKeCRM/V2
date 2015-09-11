//-----------------------------------------------------------------------
// <copyright file="DefaultErrorHandler.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

using VKeCRM.Framework.Business.Enums;
using VKeCRM.Framework.Business.Exceptions;
using VKeCRM.Common.Logging;

namespace VKeCRM.Framework.ServiceModel
{
	public class DefaultErrorHandler : IErrorHandler
	{
		internal string ServiceName
		{
			get;
			set;
		}

		#region IErrorHandler Members

		/// <summary>
		/// Indicate whether the subsquent error handler should be called.
		/// </summary>
		/// <param name="error">The exception occurred during method call.</param>
		/// <returns>false so that no subsquent error handler should be called.</returns>
		public bool HandleError(Exception error)
		{
			// logs error
			string methodName = GetServiceMethod(error);
			string errorMessage = string.Format("{0}.{1} failed in executing business logic. Message: {2}", ServiceName, methodName, error.Message);
			Logger logger = LoggerManager.GetLogger(ServiceName);

			BusinessExceptionBase ex = error as BusinessExceptionBase;
			
			if (ex == null)
			{
				logger.Error(error.Message, error);
			}
			else
			{
				switch (ex.LoggingLevel)
				{
					case LoggingLevel.Debug: logger.Debug(ex.Message, ex); break;
					case LoggingLevel.Error: logger.Error(ex.Message, ex); break;
				}
			}

			return false;
		}

		/// <summary>
		/// Capture the error occurred from business logic and put it into the repository.
		/// </summary>
		/// <param name="error"></param>
		/// <param name="version"></param>
		/// <param name="fault"></param>
		public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
            ServiceBase serviceBase = null; 
            if( OperationContext.Current != null && OperationContext.Current.InstanceContext !=null )
                serviceBase = OperationContext.Current.InstanceContext.GetServiceInstance() as ServiceBase;
			
			if (serviceBase != null)
			{
				serviceBase.HasError = true;
				serviceBase.UnknownError = error;
			}
		}

		/// <summary>
		/// Gets the service method invoked based on the exception being thrown.
		/// </summary>
		/// <param name="error">The exception being thrown.</param>
		/// <returns>The service method invoked.</returns>
		static string GetServiceMethod(Exception error)
		{
			const string WCFPrefix = "SyncInvoke";
			if (error.StackTrace != null)
			{
				int start = error.StackTrace.IndexOf(WCFPrefix);
				//Debug.Assert(start != -1);//Did they change the prefix???

				string trimedTillMethod = error.StackTrace.Substring(start + WCFPrefix.Length);
				string[] parts = trimedTillMethod.Split('(');
				return parts[0];
			}
			return null;
		}
		#endregion
	}
}
