//-----------------------------------------------------------------------
// <copyright file="UnknownException.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using VKeCRM.Framework.Business.Enums;


namespace VKeCRM.Framework.Web.Exceptions
{
    /// <summary>
    /// To handle an unknown exception
    /// </summary>
	public class UnknownException : VKeCRM.Framework.Web.Exceptions.WebExceptionBase
	{
		/// <summary>
		/// Initializes a new instance of the UnknownException class
		/// </summary>
		public UnknownException()
			: base(ExceptionErrorCode.UnknownException, true)
		{
			//// Constructor for Unknown exception
		}

        /// <summary>
        /// Initializes a new instance of the UnknownException class
        /// </summary>
        /// <param name="innerException">Inner exception</param>
		public UnknownException(Exception innerException)
			: base(ExceptionErrorCode.UnknownException, innerException, true)
		{
            //// Constructor for Unknown exception
		}
	}
}