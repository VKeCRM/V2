using System.Runtime.InteropServices;
using VKeCRM.Common.Messages;

namespace VKeCRM.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class Win32Exception : VKeCRMCommonExceptionBase
    {
        private static readonly string __message = ErrorMessages.Win32Exception;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public Win32Exception(string message) : this(message, Marshal.GetHRForLastWin32Error())
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="win32ErrorCode"></param>
        public Win32Exception(string message, int win32ErrorCode) : this(string.Format(__message, message), Marshal.GetExceptionForHR(win32ErrorCode))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public Win32Exception(string message, System.Exception ex) : base(message, ex)
        {
        }
    }
}