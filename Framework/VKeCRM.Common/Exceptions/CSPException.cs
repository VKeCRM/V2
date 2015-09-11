//-----------------------------------------------------------------------
// <copyright file="CSPException.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace VKeCRM.Common.Exceptions
{
    /// <summary>
    /// This is base exception for all Crypto Service Provider related exceptions.
    /// </summary>
    public abstract class CSPException : Win32Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CSPException class
        /// </summary>
        /// <param name="message">Exception message</param>
        public CSPException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CSPException class
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="win32ErrorCode">The system error code used to create the inner exception.</param>
        public CSPException(string message, int win32ErrorCode) : base(message, win32ErrorCode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CSPException class
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="ex">System exception</param>
        public CSPException(string message, System.Exception ex) : base(message, ex)
        {
        }

        #endregion
    }
}