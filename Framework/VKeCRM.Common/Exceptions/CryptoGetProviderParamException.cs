//-----------------------------------------------------------------------
// <copyright file="CryptoGetProviderParamException.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using VKeCRM.Common.Messages;

namespace VKeCRM.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there is a problem getting provider parameters
    /// </summary>
    public class CryptoGetProviderParamException : CSPException
    {
        #region Fields

        /// <summary>
        /// Exception message
        /// </summary>
        private static readonly string _message = ErrorMessages.CryptoGetProviderParamException;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CryptoGetProviderParamException class
        /// </summary>
        public CryptoGetProviderParamException() : base(_message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CryptoGetProviderParamException class
        /// </summary>
        /// <param name="win32ErrorCode">The system error code used to create the inner exception.</param>
        public CryptoGetProviderParamException(int win32ErrorCode) : base(_message, win32ErrorCode)
        {
        }

        #endregion // Constructors
    }
}