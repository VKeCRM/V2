using System;
using VKeCRM.Framework.Business.Enums;

namespace VKeCRM.Framework.Web.Exceptions
{
    /// <summary>
    /// Base class for handling all business logic related exceptions
    /// </summary>
    public class IntegrationException : WebExceptionBase
    {
        #region Declarations
        /// <summary>
        /// Unique error message to identify the exception
        /// </summary>
        private string _errorMessage;
        #endregion
        /// <summary>
		/// Initializes a new instance of the BusinessException class.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
        public IntegrationException(ExceptionErrorCode errorCode, string errorMessage)
			: base(errorCode, true)
		{
            _errorMessage = errorMessage;
            //// Constructor for Unknown exception
		}

        #region Properties
        /// <summary>
        /// Gets unique error message to identify the exception
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
        }
        #endregion

		public override string ToString()
		{
			return string.Concat(base.ToString(), "\r\n", "Error Code:", ErrorCode, "Message: ", ErrorMessage);
		}
    }
}
