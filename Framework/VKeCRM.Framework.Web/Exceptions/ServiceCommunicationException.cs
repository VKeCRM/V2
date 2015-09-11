using System;
using VKeCRM.Framework.Business.Enums;


namespace VKeCRM.Framework.Web.Exceptions
{
	public class ServiceCommunicationException : WebExceptionBase
	{
		/// <summary>
		/// Initializes a new instance of the CommunicationException class.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// <param name="innerException">The original Exception</param>
		public ServiceCommunicationException(ExceptionErrorCode errorCode, Exception innerException)
			: base(errorCode, innerException, true)
		{
            //// Constructor for Unknown exception
		}
	}
}
