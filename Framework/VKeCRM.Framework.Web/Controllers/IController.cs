using System;

namespace VKeCRM.Framework.Web.Controllers
{   
    /// <summary>
    /// 
    /// </summary>
	public interface IController
	{
        /// <summary>
        /// Process exception by message and exception.
        /// </summary>
        /// <param name="defaultMessage">Message need to process.</param>
        /// <param name="ex">Ex need to process.</param>
        /// <returns></returns>
		string ProcessException(string defaultMessage, System.Exception ex, VKeCRM.Common.Logging.Logger logger);   
        
        /// <summary>
        /// Transform the ServiceFault to WebException.
        /// </summary>
        /// <param name="faultException">FaultException need to transform.</param>
        /// <returns></returns>
		VKeCRM.Framework.Web.Exceptions.WebExceptionBase GetWebExceptionFromServiceFault(System.ServiceModel.FaultException faultException);
	}
}
