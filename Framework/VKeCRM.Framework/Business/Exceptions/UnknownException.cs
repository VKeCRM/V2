using System;

using VKeCRM.Framework.Business.Enums;

namespace VKeCRM.Framework.Business.Exceptions
{
	/// <summary>
	///		To handle generic exceptions.
	/// </summary>
	public class UnknownException : BusinessExceptionBase
	{
		#region Private constant variables
		/// <summary>
		///		The message for unkown exception
		/// </summary>
		private const string UnknownExceptionMessage = "An unknown error has occurred.";
		#endregion

		#region Constructor
		/// <summary>
		///		Constructor, get an instance for UnknownException.
		/// </summary>
		/// <param name="innerException">
		///		Set the value of inner exception.
		/// </param>
		public UnknownException(Exception innerException)
			: base(ExceptionErrorCode.UnknownException, UnknownExceptionMessage, innerException)
		{

		}

		/// <summary>
		///		Constructor, get an instance for UnknownException.
		/// </summary>
		/// <param name="message">
		///		Set the value of exception message.
		/// </param>
		/// <param name="innerException">
		///		Set the value of inner exception.
		/// </param>
		public UnknownException(string message, Exception innerException)
			: base(ExceptionErrorCode.UnknownException, message, innerException)
		{

		}
		#endregion
	}
}