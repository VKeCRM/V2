using VKeCRM.Common.Messages;

namespace VKeCRM.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class PublicKeyFilenameRequiredException : VKeCRMCommonExceptionBase
    {
        private static readonly string s_Message = ErrorMessages.PublicKeyFilenameRequiredException;

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PublicKeyFilenameRequiredException() : base(s_Message)
        {
        }

        #endregion // Constructors
    }
}