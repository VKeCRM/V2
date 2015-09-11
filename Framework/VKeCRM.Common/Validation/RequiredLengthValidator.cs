//-----------------------------------------------------------------------
// <copyright file="RequiredLengthValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace VKeCRM.Common.Validation
{
    /// <summary>
    /// Validator for required length
    /// </summary>
    public class RequiredLengthValidator : ValidatorBase
    {
        #region Fields

        /// <summary>
        /// Maximum required length 
        /// </summary>
        private int _maxLength = 10;

        /// <summary>
        /// Minimum requires length
        /// </summary>
        private int _minLength = 1;

        /// <summary>
        /// Value to validate for required length
        /// </summary>
        private string _valueToCheck = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RequiredLengthValidator class
        /// </summary>
        public RequiredLengthValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RequiredLengthValidator class
        /// </summary>
        /// <param name="valueToCheck">Value to check for length</param>
        public RequiredLengthValidator(string valueToCheck)
        {
            _valueToCheck = valueToCheck;
        }

        /// <summary>
        /// Initializes a new instance of the RequiredLengthValidator class
        /// </summary>
        /// <param name="valueToCheck">Value to check for length</param>
        /// <param name="minLength">Minimum length the value must have</param>
        /// <param name="maxLength">Maximum length the value can have</param>
        public RequiredLengthValidator(string valueToCheck, int minLength, int maxLength)
        {
            _valueToCheck = valueToCheck;
            _minLength = minLength;
            _maxLength = maxLength;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value to check for length
        /// </summary>
        public string ValueToCheck
        {
            get 
            { 
                return _valueToCheck; 
            }

            set 
            { 
                _valueToCheck = value; 
            }
        }

        /// <summary>
        /// Gets or sets the minimum length the value must have
        /// </summary>
        public int MinLength
        {
            get 
            { 
                return _minLength; 
            }

            set 
            { 
                _minLength = value; 
            }
        }

        /// <summary>
        /// Gets or sets the maximum length the value can have
        /// </summary>
        public int MaxLength
        {
            get 
            { 
                return _maxLength; 
            }

            set 
            { 
                _maxLength = value; 
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// To validate the value
        /// </summary>
        /// <returns>Returns a boolean value to indicate whether the value is validated</returns>
        public override bool Validate()
        {
            bool result = false;
            try
            {
                if (PreValidate())
                {
                    result = CheckValue();
                }
            }
            catch (System.Exception ex)
            {
                _errorMessage = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// To check if the value has a length within the minimum and maximum length
        /// </summary>
        /// <returns>Returns a boolean value to indicate whether the value has the required length</returns>
        protected bool CheckValue()
        {
            return _valueToCheck.Length >= _minLength && _valueToCheck.Length <= _maxLength;
        }

        /// <summary>
        /// To check if the value is pre-validated
        /// </summary>
        /// <returns>Returns a boolean value to indicate whether the value is pre-validated</returns>
        protected override bool PreValidate()
        {
            bool result = true;

            if (_valueToCheck == null || _valueToCheck.Length == 0)
            {
                _errorMessage = "Value to check is required.";
                result = false;
            }

            if (_minLength < 0 || _minLength > _maxLength)
            {
                _errorMessage = "Minimum length must be greater than 0 and less than or equal to the maximum length.";
                result = false;
            }

            if (_maxLength < 0 || _maxLength < _minLength)
            {
                _errorMessage = "Maximum length must be greater than 0 and greater than or equal to the minimum length.";
                result = false;
            }

            return result;
        }

        #endregion
    }
}