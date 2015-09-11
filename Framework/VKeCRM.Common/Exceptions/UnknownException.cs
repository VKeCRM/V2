using VKeCRM.Common.Messages;

namespace VKeCRM.Common.Exceptions
{
    /// <summary>
    /// Summary description for UnknownException.
    /// </summary>
    public sealed class UnknownException : VKeCRMCommonExceptionBase
    {
        private static readonly string __message = ErrorMessages.UnknownException;

        #region Constructors

        public UnknownException() : base(__message)
        {
        }

        public UnknownException(System.Exception innerException) : base(__message, innerException)
        {
        }

        #endregion // Constructors
    }
}