//-----------------------------------------------------------------------
// <copyright file="CryptoAquireContextException.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using VKeCRM.Common.Cryptography;
using VKeCRM.Common.Messages;

namespace VKeCRM.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there is a problem aquiring a context for a key container.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The following are possible causes: 
    /// <list type="bullet">
    /// <item><description>Key container does not exist.</description></item>
    /// <item><description>You do not have access to the key container.</description></item>
    /// <item><description>The Protected Storage Service is not running.</description></item>
    /// </list>   
    /// </para>
    /// </remarks>
    /// <seealso cref="eErrors">eErrors.NTE_BAD_KEYSET</seealso>
    public class CryptoAquireContextException : CSPException
    {
        #region Fields

        /// <summary>
        /// Exception message
        /// </summary>
        private static readonly string _message = ErrorMessages.CryptoAquireContextException;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CryptoAquireContextException class
        /// </summary>
        public CryptoAquireContextException() : base(_message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CryptoAquireContextException class
        /// </summary>
        /// <param name="win32ErrorCode">The system error code used to create the inner exception.</param>
        public CryptoAquireContextException(int win32ErrorCode) : base(_message, win32ErrorCode)
        {
        }

        #endregion
    }
}