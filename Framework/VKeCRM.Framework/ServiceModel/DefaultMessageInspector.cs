//-----------------------------------------------------------------------
// <copyright file="DefaultMessageInspector.cs" company="VKeCRM">
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
using System.Web;

using VKeCRM.Framework.Business.Enums;
using VKeCRM.Framework.Business.Exceptions;
using VKeCRM.Framework.Data;
using VKeCRM.Common.Logging;
using VKeCRM.Common.Trace;
using VKeCRM.Common.Trace.ServiceInspector;
using VKeCRM.Framework.ServiceModel.Exceptions;

namespace VKeCRM.Framework.ServiceModel
{
    /// <summary>
    /// Our own message inspector that does the generic exception wrapping.
    /// </summary>
    public class DefaultMessageInspector : IDispatchMessageInspector
    {
        #region Fields
        /// <summary>
        /// Holds the name of the service.
        /// </summary>
        internal string ServiceName
        {
            get;
            set;
        }

        #endregion

        #region IDispatchMessageInspector Members
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            return null;
        }

        /// <summary>
        /// Check if we have unexpected error occurred during the service call. If so, Create a new fault message that indicates the client the call has failed.
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            string errorCode;
            string errorReason;
            string methodName;
            // gets the current service base
            ServiceBase serviceBase = OperationContext.Current.InstanceContext.GetServiceInstance() as ServiceBase;

            if (serviceBase != null)
            {
                try
                {
                    if (serviceBase.HasError)
                    {
						BusinessExceptionBase serviceBaseError = serviceBase.UnknownError as BusinessExceptionBase;

						// Just log non-BusinessExceptionBase and BusinessExceptionBase with LoggingLevel is Error
						if (serviceBaseError == null)
						{
							LoggerManager.ServiceModelLogger.Error(serviceBase.UnknownError);
						}
						else if (serviceBaseError.LoggingLevel == LoggingLevel.Error)
						{
							LoggerManager.ServiceModelLogger.Error(serviceBaseError);
						}
						else if (serviceBaseError.LoggingLevel == LoggingLevel.Debug)
						{
							LoggerManager.ServiceModelLogger.Debug(serviceBaseError);
						}

                        NHibernateSessionManager.Instance.RollbackTransaction();
                    }
                    else
                    {
                        NHibernateSessionManager.Instance.CommitTransaction();
                    }
                }
                catch (Exception ex)
                {
                    LoggerManager.ServiceModelLogger.Error(ex.Message, ex);

					serviceBase.HasError = true;

					if (ex is NHibernate.StaleStateException)
					{
						serviceBase.UnknownError = new DataVersionNotMatchException(ex.Message);
					}
					else
					{
						serviceBase.UnknownError = ex;
					}
                }
                finally
                {
                    try
                    {
                        NHibernateSessionManager.Instance.CloseSession();
                    }
                    catch (Exception exc)
                    {
                        LoggerManager.ServiceModelLogger.Error(exc.Message, exc);
                    }
                }
            }

            if (serviceBase != null && serviceBase.HasError)
            {
                if (serviceBase.UnknownError is BusinessExceptionBase)
                {
                    // convert to business exception
                    BusinessExceptionBase businessException = serviceBase.UnknownError as BusinessExceptionBase;
                    // get method name
                    methodName = GetServiceMethod(businessException);
                    // get unknown error code 
                    errorCode = ((int)(businessException.ErrorCode)).ToString();
                    // get the associated error code
                    errorReason = string.Format("{0} failed in executing business logic. Message: {1}", methodName, serviceBase.UnknownError.Message);
                }
                else
                {
                    // get the service name and method name
                    string[] headers = reply.Headers.Action.Split('/');
                    methodName = string.Format("{0}.{1}", headers[headers.Length - 2], headers[headers.Length - 1]);
                    // get unknown error code 
                    errorCode = ((int)VKeCRM.Framework.Business.Enums.ExceptionErrorCode.DataAccessExecutionException).ToString();
                    // get unknown error reason
                    errorReason = string.Format("{0} failed when committing the transaction. Message: {1}", methodName, serviceBase.UnknownError.Message);
                    // logs error for failing in committing the transaction
                    Logger logger = LoggerManager.GetLogger(ServiceName);
                    logger.Error(errorReason, serviceBase.UnknownError);
                }

                // create fault exception
                FaultException fe = new FaultException(
                    string.Format(errorReason,
                                  methodName,
                                  serviceBase.UnknownError.Message),
                    new FaultCode(errorCode));

                MessageFault fault = fe.CreateMessageFault();
                Message newMsg = Message.CreateMessage(reply.Version, fault, null);

                // Preserve the headers of the original message
                newMsg.Headers.CopyHeadersFrom(reply);

                foreach (string propertyKey in reply.Properties.Keys)
                {
                    newMsg.Properties.Add(propertyKey, reply.Properties[propertyKey]);
                }

                // Close the original message and return new message
                reply.Close();
                reply = newMsg;
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
                string trimedTillMethod = error.StackTrace.Substring(start + WCFPrefix.Length);
                string[] parts = trimedTillMethod.Split('(');
                return parts[0];
            }
            return null;
        }
        #endregion
    }
}
