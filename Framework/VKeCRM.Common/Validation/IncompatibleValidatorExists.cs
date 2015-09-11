//-----------------------------------------------------------------------
// <copyright file="IncompatibleValidatorExists.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace VKeCRM.Common.Validation
{
    /// <summary>
    /// To determine if an imcompatible validator exists
    /// </summary>
    public sealed class IncompatibleValidatorExists : System.Exception
    {
        #region Fields
        /// <summary>
        /// Message to be displayed when a validator exists
        /// </summary>
        private static readonly string _message = "A validator with the id: '{0}' already exists. However, the type is not compatible.";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the IncompatibleValidatorExists class
        /// </summary>
        /// <param name="id">Id of validator to check if it exists</param>
        public IncompatibleValidatorExists(string id) : base(string.Format(_message, id))
        {
        }

        #endregion // Constructors
    }
}