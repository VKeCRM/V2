using VKeCRM.Common.Messages;

namespace VKeCRM.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class PrivateKeyFileExistsException : VKeCRMCommonExceptionBase
    {
        private static readonly string s_Message = ErrorMessages.PrivateKeyFileAlreadyExists;

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PrivateKeyFileExistsException(string filename) : base(string.Format(s_Message, filename))
        {
        }

        #endregion // Constructors
    }
}