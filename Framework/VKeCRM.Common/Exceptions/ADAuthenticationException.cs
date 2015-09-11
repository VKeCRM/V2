using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKeCRM.Common.Messages;

namespace VKeCRM.Common.Exceptions
{
    public class ADAuthenticationException : VKeCRMCommonExceptionBase
    {
        #region Fields

        /// <summary>
        /// Exception message
        /// </summary>
        private static readonly string _message = Messages.Messages.ADAuthentication_Failed;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the EmailFailedException class
        /// </summary>
        public ADAuthenticationException()
            : base(_message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the EmailFailedException class
        /// </summary>
        /// <param name="innerException">Inner exception</param>
        public ADAuthenticationException(System.Exception innerException)
            : base(_message, innerException)
        {
        }

        #endregion // Constructors
    }
}
