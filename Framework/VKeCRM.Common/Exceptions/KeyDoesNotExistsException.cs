//-----------------------------------------------------------------------
// <copyright file="KeyDoesNotExistsException.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using VKeCRM.Common.Messages;

namespace VKeCRM.Common.Exceptions
{
    /// <summary>
    /// To handle exception when key does not exist
    /// </summary>
    public class KeyDoesNotExistsException : VKeCRMCommonExceptionBase
    {
        #region Fields

        /// <summary>
        /// Error message
        /// </summary>
        private static readonly string _message = ErrorMessages.KeyDoesNotExistsException;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KeyDoesNotExistsException class
        /// </summary>
        /// <param name="containerName">Name of container</param>
        public KeyDoesNotExistsException(string containerName) : base(string.Format(_message, containerName))
        {
        }

        #endregion // Constructors
    }
}