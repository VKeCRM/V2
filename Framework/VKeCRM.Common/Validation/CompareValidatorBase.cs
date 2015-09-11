//-----------------------------------------------------------------------
// <copyright file="CompareValidatorBase.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace VKeCRM.Common.Validation
{
    /// <summary>
    /// Enumerator for the types of operators
    /// </summary>
    public enum ValidationCompareOperator
    {
        /// <summary>
        /// To check data type
        /// </summary>
        DataTypeCheck = 0,

        /// <summary>
        /// Indicates an equal to operator
        /// </summary>
        Equal,

        /// <summary>
        /// Indicates a greater than operator
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Indicates a greater than or equal to operator
        /// </summary>
        GreaterThanEqual,

        /// <summary>
        /// Indicates a lesser than
        /// </summary>
        LessThan,

        /// <summary>
        /// Indicates a lesser than or equal to operator
        /// </summary>
        LessThanEqual,

        /// <summary>
        /// Indicates a not equal to operator
        /// </summary>
        NotEqual
    }

    /// <summary>
    /// Validator base class for CompareValidators
    /// </summary>
    public abstract class CompareValidatorBase : ValidatorBase
    {
        #region Fields

        /// <summary>
        /// Operator to compare values
        /// </summary>
        protected ValidationCompareOperator _compareOperator = ValidationCompareOperator.Equal;

        /// <summary>
        /// First value to compare
        /// </summary>
        protected object _compareToValue = null;

        /// <summary>
        /// Second value to compare to
        /// </summary>
        protected object _valueToCompare = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CompareValidatorBase class
        /// </summary>
        public CompareValidatorBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the CompareValidatorBase class
        /// </summary>
        /// <param name="compareOperator">Operator used for comparison</param>
        /// <param name="valueToCompare">First value to compare</param>
        /// <param name="compareToValue">Second value to compare to</param>
        public CompareValidatorBase(ValidationCompareOperator compareOperator, object valueToCompare, object compareToValue)
        {
            _compareOperator = compareOperator;
            _valueToCompare = valueToCompare;
            _compareToValue = compareToValue;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the compare operator
        /// </summary>
        public ValidationCompareOperator CompareOperator
        {
            get 
            { 
                return _compareOperator; 
            }

            set 
            {
                _compareOperator = value; 
            }
        }

        /// <summary>
        /// Gets or sets the first value to compare
        /// </summary>
        public object ValueToCompare
        {
            get 
            { 
                return _valueToCompare; 
            }

            set 
            { 
                _valueToCompare = value; 
            }
        }

        /// <summary>
        /// Gets or sets the second value to compare to
        /// </summary>
        public object CompareToValue
        {
            get 
            { 
                return _compareToValue; 
            }

            set 
            { 
                CompareToValue = value; 
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// To parse the compare operator
        /// </summary>
        /// <param name="op">Compare operator to parse</param>
        /// <returns>Returns the parsed compare operator</returns>
        public static ValidationCompareOperator ParseOperator(System.Web.UI.WebControls.ValidationCompareOperator op)
        {
            return (ValidationCompareOperator)Enum.Parse(typeof(ValidationCompareOperator), op.ToString());
        }

        /// <summary>
        /// To validate if the comparison is valid
        /// </summary>
        /// <returns>Returns a boolean value indicating whether the comparison result is validated</returns>
        public override bool Validate()
        {
            bool result = false;
            try
            {
                if (PreValidate())
                {
                    result = IsCompareValid();
                }
            }
            catch (System.Exception ex)
            {
                _errorMessage = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Signature of abstract method to compare first value and second value
        /// </summary>
        /// <returns>Returns the result of the comparison</returns>
        protected abstract int CompareTo();

        /// <summary>
        /// To check whether the comparison result is valid
        /// </summary>
        /// <returns>Returns a boolean value indicating whether the comparison is valid</returns>
        private bool IsCompareValid()
        {
            int compareResult = CompareTo();

            if (_compareOperator == ValidationCompareOperator.Equal && compareResult == 0)
            {
                return true;
            }

            if (_compareOperator == ValidationCompareOperator.NotEqual && compareResult != 0)
            {
                return true;
            }

            if ((_compareOperator == ValidationCompareOperator.LessThan) && (compareResult < 0))
            {
                return true;
            }

            if (_compareOperator == ValidationCompareOperator.LessThanEqual && compareResult <= 0)
            {
                return true;
            }

            if (_compareOperator == ValidationCompareOperator.GreaterThan && compareResult > 0)
            {
                return true;
            }

            if (_compareOperator == ValidationCompareOperator.GreaterThanEqual && compareResult >= 0)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}