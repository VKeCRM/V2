//-----------------------------------------------------------------------
// <copyright file="PasswordStrength.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Text.RegularExpressions;

namespace VKeCRM.Common.Cryptography
{
    /// <summary>
    /// Utility class to rate the strength a perspective password.
    /// </summary>
    public class PasswordStrength
    {
        #region Constants

        /// <summary>
        /// Represents the maximum rating value a password may be give.
        /// </summary>
        public const int MaxRating = NumbersRating + LowercaseRating + UppercaseRating + PunctuationRating;

        /// <summary>
        /// Represents the rating value for a password containing numbers.
        /// </summary>
        private const int NumbersRating = 10;

        /// <summary>
        /// Represents the rating value for a password containing punctuation marks.
        /// </summary>
        private const int PunctuationRating = 31;

        /// <summary>
        /// Represents the rating value for a password containing uppercase characters.
        /// </summary>
        private const int UppercaseRating = 26;

        #endregion // Constants

        #region Constructors

        #endregion // Constructors

        #region Private Fields

        /// <summary>
        /// Represents the rating value for a password containing lowercase characters.
        /// </summary>
        private const int LowercaseRating = 26;

        /// <summary>
        /// The password to rate.
        /// </summary>
        private string password = string.Empty;

        /// <summary>
        /// The rated strength of the current password.
        /// </summary>
        private double strength;

        /// <summary>
        /// Initializes a new instance of the PasswordStrength class. The new instance will perform an initial rating.
        /// </summary>
        /// <param name="password">password for constructor</param>
        public PasswordStrength(string password)
        {
            Password = password;
        }

        #endregion // Private Fields

        #region Public Properties

        /// <summary>
        /// Sets the password to rate.
        /// </summary>
        public string Password
        {
            set
            {
                if (!string.Equals(password, value))
                {
                    password = value;
                    Rate();
                }
            }
        }

        /// <summary>
        /// Gets the equivalent bit strength of the current password.
        /// </summary>
        public int BitSize
        {
            get { return (int) Math.Round(strength, 0); }
        }

        /// <summary>
        /// Gets the Strength classification of the current password.
        /// </summary>
        public PasswordRating Strength
        {
            get
            {
                PasswordRating rating = PasswordRating.Weak;

                if (strength <= 32)
                {
                    rating = PasswordRating.Weak;
                }
                else if (strength <= 64)
                {
                    rating = PasswordRating.Mediocre;
                }
                else if (strength <= 128)
                {
                    rating = PasswordRating.Ok;
                }
                else if (strength > 128)
                {
                    rating = PasswordRating.Strong;
                }

                return rating;
            }
        }

        #endregion // Public Properties

        #region Rating Helper Methods

        /// <summary>
        /// Evaluates the existence of numeric characters in a string.
        /// </summary>
        /// <param name="text">The text to evaluate.</param>
        /// <returns><b><see langword="true"/></b> if the text contains at least one numeric character.</returns>
        private static bool ContainsNumbers(string text)
        {
            Regex pattern = new Regex(@"[\d]");
            return pattern.IsMatch(text);
        }

        /// <summary>
        /// Evaluates the existence of lowercase characters in a string.
        /// </summary>
        /// <param name="text">The text to evaluate.</param>
        /// <returns><b><see langword="true"/></b> if the text contains at least one lowercase character.</returns>
        private static bool ContainsLowerCaseChars(string text)
        {
            Regex pattern = new Regex("[a-z]");
            return pattern.IsMatch(text);
        }

        /// <summary>
        /// Evaluates the existence of uppercase characters in a string.
        /// </summary>
        /// <param name="text">The text to evaluate.</param>
        /// <returns><b><see langword="true"/></b> if the text contains at least one uppercase character.</returns>
        private static bool ContainsUpperCaseChars(string text)
        {
            Regex pattern = new Regex("[A-Z]");
            return pattern.IsMatch(text);
        }

        /// <summary>
        /// Evaluates the existence of punctuation mark characters in a string.
        /// </summary>
        /// <param name="text">The text to evaluate.</param>
        /// <returns><b><see langword="true"/></b> if the text contains at least one punctuation mark character.</returns>
        private static bool ContainsPunctuation(string text)
        {
            // regular expression include _ as a valid char for alphanumeric.. 
            // so we need to explicity state that its considered punctuation.
            Regex pattern = new Regex(@"[\W|_]");
            return pattern.IsMatch(text);
        }

        /// <summary>
        /// Rates the current password and persists the result in the Strength property.
        /// </summary>
        private void Rate()
        {
            int rating = 0;
            if (ContainsNumbers(password))
            {
                rating += NumbersRating;
            }

            if (ContainsLowerCaseChars(password))
            {
                rating += LowercaseRating;
            }

            if (ContainsUpperCaseChars(password))
            {
                rating += UppercaseRating;
            }

            if (ContainsPunctuation(password))
            {
                rating += PunctuationRating;
            }

            strength = Math.Log(Math.Pow(rating, password.Length)) / Math.Log(2);
        }

        #endregion // Rating Helper Methods
    }
}