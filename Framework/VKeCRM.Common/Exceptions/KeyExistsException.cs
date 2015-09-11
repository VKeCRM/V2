//-----------------------------------------------------------------------
// <copyright file="KeyExistsException.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using VKeCRM.Common.Messages;

namespace VKeCRM.Common.Exceptions
{
    /// <summary>
    /// To handle exception if key already exists
    /// </summary>
    public class KeyExistsException : VKeCRMCommonExceptionBase
    {
        #region Fields

        /// <summary>
        /// Error message
        /// </summary>
        private static readonly string _message = ErrorMessages.KeyExistsException;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KeyExistsException class
        /// </summary>
        /// <param name="containerName">Name of container</param>
        public KeyExistsException(string containerName) : base(string.Format(_message, containerName))
        {
        }

        #endregion // Constructors
    }
}