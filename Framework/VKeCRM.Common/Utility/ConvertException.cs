//-----------------------------------------------------------------------
// <copyright file="ConvertException.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

using VKeCRM.Common.Exceptions;
using VKeCRM.Common.Messages;

namespace VKeCRM.Common.Utility
{
    /// <summary>
    /// To convert exception to error message
    /// </summary>
    public sealed class ConvertException : VKeCRMCommonExceptionBase
    {
        #region Fields

        /// <summary>
        /// Error message for exception
        /// </summary>
        private static readonly string __message = ErrorMessages.ConvertException;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ConvertException class
        /// </summary>
        /// <param name="message">Error message</param>
        public ConvertException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ConvertException class
        /// </summary>
        /// <param name="operation">Operation to perform</param>
        /// <param name="sourceValue">Source of exception</param>
        /// <param name="innerException">Inner exception</param>
        public ConvertException(string operation, string sourceValue, System.Exception innerException) : this(string.Format(__message, operation, sourceValue), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ConvertException class
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <param name="innerException">Inner exception</param>
        private ConvertException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion // Constructors
    }
}