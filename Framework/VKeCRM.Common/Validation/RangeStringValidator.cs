//-----------------------------------------------------------------------
// <copyright file="RangeStringValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace VKeCRM.Common.Validation
{
    /// <summary>
    /// Validate range of string values
    /// </summary>
    public class RangeStringValidator : RangeValidatorBase
    {
        #region Fields
        /// <summary>
        /// A boolean value that indicates whether to ignore case
        /// </summary>
        private bool _ignoreCase = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RangeStringValidator class
        /// </summary>
        public RangeStringValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RangeStringValidator class
        /// </summary>
        /// <param name="ignoreCase">A boolean value that indicates whether to ignore case</param>
        /// <param name="valueToCompare">Value to compare for string range</param>
        /// <param name="min">Minimum value for string</param>
        /// <param name="max">Maximum value for string</param>
        public RangeStringValidator(bool ignoreCase, object valueToCompare, object min, object max)
        {
            _ignoreCase = ignoreCase;
            _valueToCompare = Convert.ToString(valueToCompare);
            _min = Convert.ToString(min);
            _max = Convert.ToString(max);
        }

        /// <summary>
        /// Initializes a new instance of the RangeStringValidator class
        /// </summary>
        /// <param name="ignoreCase">A boolean value that indicates whether to ignore case</param>
        /// <param name="valueToCompare">Value to compare for string range</param>
        /// <param name="min">Minimum value for string</param>
        /// <param name="max">Maximum value for string</param>
        public RangeStringValidator(bool ignoreCase, string valueToCompare, string min, string max) : base(valueToCompare, min, max)
        {
            _ignoreCase = ignoreCase;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value to compare 
        /// </summary>
        public new string ValueToCompare
        {
            get 
            { 
                return base.ValueToCompare as string; 
            }

            set 
            { 
                base.ValueToCompare = value; 
            }
        }

        /// <summary>
        /// Gets or sets the minimum value for string
        /// </summary>
        public new string Min
        {
            get 
            { 
                return base.Min as string; 
            }

            set 
            { 
                base.Min = value; 
            }
        }

        /// <summary>
        /// Gets or sets the maximum value for string
        /// </summary>
        public new string Max
        {
            get 
            { 
                return base.Max as string; 
            }

            set 
            { 
                base.Max = value; 
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compare if the value falls between the minimum and maximum string range
        /// </summary>
        /// <returns>Returns a boolean value that indicates whether the value is within the string range</returns>
        protected override bool Compare()
        {
            string valueToCompare = _valueToCompare as string;
            string min = _min as string;
            string max = _max as string;

            if (_ignoreCase)
            {
                if (valueToCompare != null)
                {
                    valueToCompare = valueToCompare.ToUpper();
                }

                if (min != null)
                {
                    min = min.ToUpper();
                }

                if (max != null)
                {
                    max = max.ToUpper();
                }
            }

            if (valueToCompare != null)
            {
                return valueToCompare.CompareTo(min) >= 0 && valueToCompare.CompareTo(max) <= 0;
            }

            throw new System.Exception();
        }

        /// <summary>
        /// Pre-validate string
        /// </summary>
        /// <returns>Returns a boolean value that indicates whether the string value is pre-validated</returns>
        protected override bool PreValidate()
        {
            bool result = true;

            // check datatypes
            if (ValueToCompare == null)
            {
                _errorMessage = "Value to compare is required.";
                result = false;
            }

            if (Min == null)
            {
                _errorMessage = "Minimum value to compare to is required.";
                result = false;
            }

            if (Max == null)
            {
                _errorMessage = "Maximum value to compare to is required.";
                result = false;
            }

            return result;
        }

        #endregion
    }
}