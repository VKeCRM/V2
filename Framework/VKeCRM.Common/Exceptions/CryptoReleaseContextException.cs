//-----------------------------------------------------------------------
// <copyright file="CryptoReleaseContextException.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using VKeCRM.Common.Messages;

namespace VKeCRM.Common.Exceptions
{
    /// <summary>
    /// To handle exceptions that arise when releasing context
    /// </summary>
    public class CryptoReleaseContextException : CSPException
    {
        #region Fields

        /// <summary>
        /// Exception message
        /// </summary>
        private static readonly string _message = ErrorMessages.CryptoReleaseContextException;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CryptoReleaseContextException class
        /// </summary>
        public CryptoReleaseContextException() : base(_message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CryptoReleaseContextException class
        /// </summary>
        /// <param name="win32ErrorCode">The system error code used to create the inner exception.</param>
        public CryptoReleaseContextException(int win32ErrorCode) : base(_message, win32ErrorCode)
        {
        }

        #endregion // Constructors
    }
}