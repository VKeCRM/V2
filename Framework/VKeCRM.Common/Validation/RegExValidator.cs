//-----------------------------------------------------------------------
// <copyright file="RegExValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace VKeCRM.Common.Validation
{
    /// <summary>
    /// Validator for Regular expressions
    /// </summary>
    public class RegExValidator : ValidatorBase
    {
        #region Fields

        /// <summary>
        /// Regular expression to check value against
        /// </summary>
        private string _expression = string.Empty;

        /// <summary>
        /// A boolean value to indicate whether to ignore case or not
        /// </summary>
        private bool _ignoreCase = true;

        /// <summary>
        /// A boolean value to indicate if the regular expression is over multiple lines
        /// </summary>
        private bool _multiLine = false;

        /// <summary>
        /// The value to validate
        /// </summary>
        private string _valueToCheck = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the RegExValidator class
        /// </summary>
        public RegExValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RegExValidator class
        /// </summary>
        /// <param name="valueToCheck">Value to validate</param>
        /// <param name="expression">Regular expression to check value against</param>
        /// <param name="ignoreCase">A boolean value to indicate whether to ignore case or not</param>
        /// <param name="multiLine">A boolean value to indicate if the regular expression is over multiple lines</param>
        public RegExValidator(string valueToCheck, string expression, bool ignoreCase, bool multiLine)
        {
            _valueToCheck = valueToCheck;
            _expression = expression;
            _ignoreCase = ignoreCase;
            _multiLine = multiLine;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value to validate
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
        /// Gets or sets the Regular expression to check value against
        /// </summary>
        public string Expression
        {
            get 
            { 
                return _expression; 
            }

            set 
            { 
                _expression = value; 
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore case or not
        /// </summary>
        public bool IgnoreCase
        {
            get 
            { 
                return _ignoreCase; 
            }

            set 
            { 
                _ignoreCase = value; 
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the regular expression is over multiple lines
        /// </summary>
        public bool MultiLine
        {
            get 
            { 
                return _multiLine; 
            }

            set 
            { 
                _multiLine = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// To validate the value to check
        /// </summary>
        /// <returns>Returns a boolean value that indicates whether the value is validated</returns>
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
        /// To check the value for ignoring case and see if it contains multiple lines
        /// </summary>
        /// <returns>Returns a boolean value that indicated whether the value has been set to ignore case and/or multi-lines</returns>
        protected bool CheckValue()
        {
            RegexOptions options = RegexOptions.None;
            Regex re;

            if (_ignoreCase)
            {
                options = RegexOptions.IgnoreCase;
            }

            if (_multiLine)
            {
                options |= RegexOptions.Multiline;
            }

            re = new Regex(_expression, options);

            return re.IsMatch(_valueToCheck);
        }

        /// <summary>
        /// Pre-validate the value to check
        /// </summary>
        /// <returns>Returns a boolean value that indicates whether the value is pre-validated</returns>
        protected override bool PreValidate()
        {
            bool result = true;

            if (_valueToCheck == null || _valueToCheck.Length == 0)
            {
                _errorMessage = "Value to check is required.";
                result = false;
            }

            if (_expression == null || _expression.Length == 0)
            {
                _errorMessage = "Regular expression is required.";
                result = false;
            }

            return result;
        }
        
        #endregion
    }
}