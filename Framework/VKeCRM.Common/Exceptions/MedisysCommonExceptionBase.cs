using System;

namespace VKeCRM.Common.Exceptions
{
    /// <summary>
    /// Summary description for VKeCRMCommonExceptionBase.
    /// </summary>
    public abstract class VKeCRMCommonExceptionBase : ApplicationException
    {
        #region Constructors

        protected VKeCRMCommonExceptionBase(string message) : base(message)
        {
        }

        protected VKeCRMCommonExceptionBase(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        #endregion // Constructors
    }
}