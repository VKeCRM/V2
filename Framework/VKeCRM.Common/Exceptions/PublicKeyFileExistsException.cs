using VKeCRM.Common.Messages;

namespace VKeCRM.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class PublicKeyFileExistsException : VKeCRMCommonExceptionBase
    {
        private static readonly string s_Message = ErrorMessages.PublicKeyFileAlreadyExists;

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PublicKeyFileExistsException(string filename) : base(string.Format(s_Message, filename))
        {
        }

        #endregion // Constructors
    }
}