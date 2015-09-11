using System;

using VKeCRM.Framework.Business.Enums;

namespace VKeCRM.Framework.Web.Exceptions
{
	/// <summary>
	/// To handle exception when account gets locked
	/// </summary>
	public class LoggerManagerNotInitializedException : VKeCRM.Framework.Web.Exceptions.WebExceptionBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the AccountLockedException class
		/// </summary>
		public LoggerManagerNotInitializedException()
			: base(ExceptionErrorCode.LoggerManagerNotInitialized)
		{
		}

		/// <summary>
		/// Initializes a new instance of the AccountLockedException class
		/// </summary>
		/// <param name="innerException">Inner exception</param>
		public LoggerManagerNotInitializedException(System.Exception innerException)
			: base(ExceptionErrorCode.LoggerManagerNotInitialized, innerException)
		{
		}

		#endregion
	}
}