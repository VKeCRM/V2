//-----------------------------------------------------------------------
// <copyright file="BusinessExceptionBase.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using VKeCRM.Framework.Business.Enums;

namespace VKeCRM.Framework.Business.Exceptions
{
    /// <summary>
    /// Base class for handling all business logic related exceptions
    /// </summary>
	public abstract class BusinessExceptionBase : System.Exception
	{
		#region Declarations
		/// <summary>
		/// Unique error code to identify the exception
		/// </summary>
		private ExceptionErrorCode _errorCode;
		#endregion

		#region Constructors

		/// <summary>
        /// Initializes a new instance of the BusinessExceptionBase class
        /// </summary>
		/// <param name="errorCode">Exception error code</param>
        /// <param name="message">Exception message</param>
		protected BusinessExceptionBase(ExceptionErrorCode errorCode, string message)
			: base(message)
		{
			_errorCode = errorCode;
		}

        /// <summary>
        /// Initializes a new instance of the BusinessExceptionBase class
        /// </summary>
		/// <param name="errorCode">Exception error code</param>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
		protected BusinessExceptionBase(ExceptionErrorCode errorCode, string message, System.Exception innerException)
			: base(message, innerException)
		{
			_errorCode = errorCode;
		}

		#endregion // Constructors

		#region Properties
		/// <summary>
		/// Gets unique error code to identify the exception
		/// </summary>
		public ExceptionErrorCode ErrorCode
		{
			get
			{
				return _errorCode;
			}
		}

		/// <summary>
		///		Gets LoggingLevel for the current exception
		/// </summary>
		public virtual LoggingLevel LoggingLevel
		{
			get
			{
				return LoggingLevel.Error;
			}
		}
		#endregion
	}
}
