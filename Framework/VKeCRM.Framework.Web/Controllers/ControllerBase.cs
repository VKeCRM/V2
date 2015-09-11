using System;
using System.Web;
using System.ServiceModel;

using VKeCRM.Common.Logging;
using VKeCRM.Framework.Web.Exceptions;
using VKeCRM.Common.Utility;
using VKeCRM.Framework.Web.State;
using VKeCRM.Framework.Business.Enums;

namespace VKeCRM.Framework.Web.Controllers
{
    /// <summary>
    /// Root for all controller objects.
    /// </summary>
    public abstract class ControllerBase : IController
    {

		/// <summary>
		/// The following section just provides shortcuts to the asp.net server objects
		/// </summary>
		#region HttpContext Server Objects

		public HttpRequest Request
		{
			get
			{
				return System.Web.HttpContext.Current.Request;
			}
		}

		public HttpResponse Response
		{
			get
			{
				return System.Web.HttpContext.Current.Response;
			}
		}

		#endregion // Http Server Objects


        #region LoggerManager

        private static LoggerManager _manager;
        public static LoggerManager Logging
        {
            get
            {
                // Load it if it has not been loaded
                if (_manager == null)
                {
                    // Load LoggerManager from Application Bag
                    _manager = ApplicationState.LoggerManager;
                    
                    // Raise an exception if it has not been initialized.
                    if (_manager==null)
                        throw new LoggerManagerNotInitializedException();
                }

                // return it
                return _manager;
            }
        }

        #endregion // LoggerManager

        #region Error Handling
		/// <summary>
		/// This method translates an exception from the service layer to an exception for the presentation layer
		/// </summary>
		/// <param name="faultException">The fault exception from the service layer</param>
		/// <returns>Returns the exception for the service fault</returns>            
		public WebExceptionBase GetWebExceptionFromServiceFault(System.ServiceModel.FaultException faultException)
		{
			int errorCodeInInt;
			WebExceptionBase webException = null;

			if (int.TryParse(faultException.Code.Name, out errorCodeInInt))
			{
				ExceptionErrorCode errorCode = (ExceptionErrorCode)errorCodeInInt;
				if (errorCode != ExceptionErrorCode.UnknownException)
				{
					webException = new BusinessException(errorCode);
				}
				else
				{
					webException = new UnknownException();
				}
			}
			else
			{
				webException = new UnknownException(faultException);
			}

			return webException;
		}

		public static void CleanUpServiceClient(ICommunicationObject client, IClientChannel channel)
		{
			WcfClientHelper.CleanUpServiceClient(client, channel);
		}
        #endregion

		#region Abstract Methods
		/// <summary>
		/// Returns the proper string to be displayed in the presentation layer when an exception occurs. 
		/// This should be used for all called to the controller where a potential error can occur.
		/// </summary>
		/// <param name="messageToDisplay">Default message to display in presentation layer</param>
		/// <param name="ex">Exception to process</param>
		/// <param name="logger">The specific application logger passing from the inheriting contoller base class</param>
		/// <returns>Returns the message to display</returns>
		//public abstract string ProcessException(string messageToDisplay, System.Exception ex);
		public string ProcessException(string messageToDisplay, Exception ex, Logger logger)
		{
			Exception currentException = ex;

			if (!(currentException is BusinessException))
			{
				if (!(currentException is WebExceptionBase))
				{
					if (currentException is System.Security.SecurityException)
					{
						currentException = new ServiceCommunicationException(ExceptionErrorCode.SecurityException, currentException);
					}
					else if (currentException is System.ServiceModel.CommunicationObjectFaultedException)
					{
						currentException = new ServiceCommunicationException(ExceptionErrorCode.CommunicationObjectFaultedException, currentException);
					}
					else if (currentException is System.ServiceModel.Security.MessageSecurityException)
					{
						currentException = new ServiceCommunicationException(ExceptionErrorCode.MessageSecurityException, currentException);
					}
					else if (currentException is System.TimeoutException)
					{
						currentException = new ServiceCommunicationException(ExceptionErrorCode.TimeoutException, currentException);
					}
					else if (currentException is System.ServiceModel.CommunicationException)
					{
						currentException = new ServiceCommunicationException(ExceptionErrorCode.CommunicationException, currentException);
					}
					else if (currentException is System.ObjectDisposedException)
					{
						currentException = new ServiceCommunicationException(ExceptionErrorCode.ObjectDisposedException, currentException);
					}
					else
					{
						currentException = new UnknownException(currentException);
					}
				}

				logger.Error(currentException.ToString());
			}

			//Add error code to DisplayMessage
			messageToDisplay = string.Concat(messageToDisplay, string.Format(" (ErrorCode: {0})", (int)((WebExceptionBase)currentException).ErrorCode));

			return messageToDisplay;
		}		
		#endregion
	}
}
