//-----------------------------------------------------------------------
// <copyright file="PasswordRating.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace VKeCRM.Common.Cryptography
{
    /// <summary>
    /// Represents the password strength classifications.
    /// </summary>
    public enum PasswordRating
    {
        /// <summary>
        /// Represents the lowest rating.
        /// </summary>
        Weak,

        /// <summary>
        /// Represents an average rating.
        /// </summary>
        Mediocre,

        /// <summary>
        /// Represents an above average rating.
        /// </summary>
        Ok,

        /// <summary>
        /// Represents the strongest rating.
        /// </summary>
        Strong
    }
}