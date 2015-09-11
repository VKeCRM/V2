//-----------------------------------------------------------------------
// <copyright file="EmailFailedException.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using VKeCRM.Common.Messages;

namespace VKeCRM.Common.Exceptions
{
    /// <summary>
    /// To handle exceptions when user welcome emails fail
    /// </summary>
    public class EmailFailedException : VKeCRMCommonExceptionBase
    {
        #region Fields

        /// <summary>
        /// Exception message
        /// </summary>
        private static readonly string _message = ErrorMessages.EmailFailedException;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the EmailFailedException class
        /// </summary>
        public EmailFailedException() : base(_message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the EmailFailedException class
        /// </summary>
        /// <param name="innerException">Inner exception</param>
        public EmailFailedException(System.Exception innerException) : base(_message, innerException)
        {
        }

        #endregion // Constructors
    }
}