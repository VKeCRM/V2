//-----------------------------------------------------------------------
// <copyright file="PhoneNumberValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace VKeCRM.Common.Validation
{
    /// <summary>
    /// To validate phone number
    /// </summary>
    public class PhoneNumberValidator : ValidatorBase
    {
        #region Fields

        /// <summary>
        /// Area code for phone number
        /// </summary>
        private string _areaCode = string.Empty;

        /// <summary>
        /// To indicate if phone number is a required field
        /// </summary>
		private bool _isRequired = true;

        /// <summary>
        /// Prefix for phone number
        /// </summary>
		private string _prefix = string.Empty;

        /// <summary>
        /// Suffix for phone number
        /// </summary>
		private string _suffix = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PhoneNumberValidator class
        /// </summary>
        public PhoneNumberValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the PhoneNumberValidator class
        /// </summary>
        /// <param name="areaCode">Area code for phone number</param>
        /// <param name="prefix">Prefix for phone number</param>
        /// <param name="suffix">Suffix for phone number</param>
        public PhoneNumberValidator(string areaCode, string prefix, string suffix)
        {
            if (areaCode != null)
            {
                _areaCode = areaCode.Trim();
            }

            if (prefix != null)
            {
                _prefix = prefix.Trim();
            }

            if (suffix != null)
            {
                _suffix = suffix.Trim();
            }
        }

        /// <summary>
        /// Initializes a new instance of the PhoneNumberValidator class
        /// </summary>
        /// <param name="areaCode">Area code for phone number</param>
        /// <param name="prefix">Prefix for phone number</param>
        /// <param name="suffix">Suffix for phone number</param>
        /// <param name="isRequired">To indicate if phone number is a required field</param>
        public PhoneNumberValidator(string areaCode, string prefix, string suffix, bool isRequired)
        {
            if (areaCode != null)
            {
                _areaCode = areaCode.Trim();
            }

            if (prefix != null)
            {
                _prefix = prefix.Trim();
            }

            if (suffix != null)
            {
                _suffix = suffix.Trim();
            }

			_isRequired = isRequired;
        }

        #endregion

        #region Methods

        /// <summary>
        /// To validate a phone number
        /// </summary>
        /// <returns>Returns a boolean value to indicate whether phone number is validated</returns>
        public override bool Validate()
        {
            bool result = false;
            try
            {
                if (PreValidate())
                {
                    result = IsValidPhoneNumber();
                }
            }
            catch (System.Exception ex)
            {
                _errorMessage = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// To check if phone number is valid
        /// </summary>
        /// <returns>Returns a boolean value to indicate whether phone number is valid</returns>
        protected bool IsValidPhoneNumber()
        {
            bool result = true;

            _errorMessage = string.Empty;

            if ((_areaCode == null || _areaCode.Length == 0) && (_prefix == null || _prefix.Length == 0) && (_suffix == null || _suffix.Length == 0) && !_isRequired)
            {
                return true;
            }

            // All or Nothing regardless of required
            if (_areaCode == null || _areaCode.Length == 0)
            {
                _errorMessage += "Area code is required.\n";
            }
            else if (_areaCode.Length > 3 || !new RegExValidator(_areaCode, @"\d{3}", true, false).Validate())
            {
                _errorMessage += "Area code must be three digits long.\n";
            }

            if (_prefix == null || _prefix.Length == 0)
            {
                _errorMessage += "Prefix is required.\n";
            }
            else if (_prefix.Length > 3 || !new RegExValidator(_prefix, @"\d{3}", true, false).Validate())
            {
                _errorMessage += "Prefix must be three digits long.\n";
            }

            if (_suffix == null || _suffix.Length == 0)
            {
                _errorMessage += "Suffix is required.\n";
            }
            else if (_suffix.Length > 4 || !new RegExValidator(_suffix, @"\d{4}", true, false).Validate())
            {
                _errorMessage += "Suffix must be four digits long.\n";
            }

            if (_errorMessage.Length > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Pre-validate the phone number
        /// </summary>
        /// <returns>Returns a boolean value to indicate whether phone number is pre-validated</returns>
        protected override bool PreValidate()
        {
            bool result = true;
            return result;
        }

        #endregion
    }
}