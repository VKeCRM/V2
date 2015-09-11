using VKeCRM.Common.Messages;

namespace VKeCRM.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class PrivateKeyFilenameRequiredException : VKeCRMCommonExceptionBase
    {
        private static readonly string s_Message = ErrorMessages.PrivateKeyFilenameRequiredException;

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PrivateKeyFilenameRequiredException() : base(s_Message)
        {
        }

        #endregion // Constructors
    }
}