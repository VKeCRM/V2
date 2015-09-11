using System;
using System.Resources;
using System.Threading;
using VKeCRM.Framework.Business.Enums;


namespace VKeCRM.Framework.Web.Exceptions
{
    public class WebExceptionBase : System.Exception
    {
        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="WebExceptionBase"/> class.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// <param name="doNotGetMsgFromResource">No matter false or true set to this parameter, we would't get the message from Resource.</param>
		protected WebExceptionBase(ExceptionErrorCode errorCode, bool doNotGetMsgFromResource)
		{
			_errorCode = errorCode;
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="WebExceptionBase"/> class.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// <param name="innerException">The inner exception.</param>
		/// <param name="doNotGetMsgFromResource">No matter false or true set to this parameter, we would't get the message from Resource.</param>
		protected WebExceptionBase(ExceptionErrorCode errorCode, System.Exception innerException, bool doNotGetMsgFromResource)
			: base("", innerException)
		{
			_errorCode = errorCode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WebExceptionBase"/> class.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// <param name="info">The info.</param>
    	protected WebExceptionBase(ExceptionErrorCode errorCode, params string[] info)
    		: base(GetMessageFromResource(errorCode, info))
        {
    		_errorCode = errorCode;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="WebExceptionBase"/> class.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// <param name="innerException">The inner exception.</param>
		/// <param name="info">The info.</param>
		protected WebExceptionBase(ExceptionErrorCode errorCode, System.Exception innerException, params string[] info)
			: base(GetMessageFromResource(errorCode, info), innerException)
        {
			_errorCode = errorCode;
        }
        
        #endregion // Constructors

		#region Fields and Proerty

		/// <summary>
		/// Unique error code to identify the exception
		/// </summary>
		private ExceptionErrorCode _errorCode;

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
		#endregion

		#region Help method
		/// <summary>
		/// Get the resource string from invoked assembly by the key.
		/// </summary>
		/// <param name="errorCode">Errorcode for the exception</param>
		/// <param name="info">String parameters for error message.</param>
		/// <returns>Error message.</returns>
		private static string GetMessageFromResource(ExceptionErrorCode errorCode, params string[] info)
		{

			System.Resources.ResourceManager rm = new global::System.Resources.ResourceManager("Resources.ErrorMessages",
																							   global::System.Reflection.
																								Assembly.Load(
																								"App_GlobalResources"));
			//Try to get specific error message from Resource.
			string errorMessage = rm.GetString(errorCode.ToString());

			if (string.IsNullOrEmpty(errorMessage))
			{
				try
				{
					errorMessage = string.Format(rm.GetString(ExceptionErrorCode.ErrorMessageNotFound.ToString()), ((int)errorCode).ToString());
				}
				catch
				{
					errorMessage = string.Format("Failed to get the ErrorMessage for ErrorCode: {0} and {1}",
												 ((int)errorCode).ToString(), ((int)ExceptionErrorCode.ErrorMessageNotFound).ToString());
				}
			}
			else
			{
				if (info != null && info.Length != 0)
				{//Whether we should use the format method to combine string.
					errorMessage = string.Format(errorMessage, info);
				}
			}

			return errorMessage;

		}

		public string CustomerMessage
		{
			get;
			set;
		}

		#endregion
	}
}
