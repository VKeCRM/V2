using System;
using VKeCRM.Framework.Business.Enums;


namespace VKeCRM.Framework.Web.Exceptions
{
	public class BusinessException : WebExceptionBase
	{
		/// <summary>
		/// Initializes a new instance of the BusinessException class.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		public BusinessException(ExceptionErrorCode errorCode)
			: base(errorCode, true)
		{
            //// Constructor for Unknown exception
		}

	

	}
}
