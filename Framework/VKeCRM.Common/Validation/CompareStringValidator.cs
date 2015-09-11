//-----------------------------------------------------------------------
// <copyright file="CompareStringValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace VKeCRM.Common.Validation
{
	/// <summary>
	/// Validator class for string comparisons
	/// </summary>
	public class CompareStringValidator : CompareValidatorBase
    {
        #region Fields

        /// <summary>
        /// A boolean value to indicate whether to ignore case or not
        /// </summary>
        protected bool _ignoreCase = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CompareStringValidator class
        /// </summary>
        public CompareStringValidator()
		{
		}

        /// <summary>
        /// Initializes a new instance of the CompareStringValidator class
        /// </summary>
        /// <param name="ignoreCase">A boolean value to indicate whether to ignore case or not</param>
        /// <param name="compareOperator">Enumerator for types of operators</param>
        /// <param name="valueToCompare">First string to compare</param>
        /// <param name="compareToValue">Second string to compare</param>
		public CompareStringValidator(bool ignoreCase, ValidationCompareOperator compareOperator, object valueToCompare, object compareToValue)
		{
			_ignoreCase = ignoreCase;
			_compareOperator = compareOperator;
			_valueToCompare = Convert.ToString(valueToCompare);
			_compareToValue = Convert.ToString(compareToValue);
		}

        /// <summary>
        /// Initializes a new instance of the CompareStringValidator class
        /// </summary>
        /// <param name="ignoreCase">A boolean value to indicate whether to ignore case or not</param>
        /// <param name="compareOperator">Enumerator for types of operators</param>
        /// <param name="valueToCompare">First string to compare</param>
        /// <param name="compareToValue">Second string to compare</param>
		public CompareStringValidator(bool ignoreCase, ValidationCompareOperator compareOperator, string valueToCompare, string compareToValue)
			: base(compareOperator, valueToCompare, compareToValue)
		{
			_ignoreCase = ignoreCase;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the first string to compare
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
        /// Gets or sets the second string to compare
        /// </summary>
		public new string CompareToValue
		{
			get 
            { 
                return base.CompareToValue as string; 
            }

			set 
            { 
                base.CompareToValue = value; 
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// To get the string to compare to
        /// </summary>
        /// <returns>Returns value to compare to</returns>
		protected override int CompareTo()
		{
			string valueToCompare = _valueToCompare as string;
			string compareToValue = _compareToValue as string;

			if (_ignoreCase)
			{
                if (valueToCompare != null)
                {
                    valueToCompare = valueToCompare.ToUpper();
                }

                if (compareToValue != null)
                {
                    compareToValue = compareToValue.ToUpper();
                }
			}

            if (valueToCompare != null)
            {
                return valueToCompare.CompareTo(compareToValue);
            }

			throw new System.Exception();
		}

        /// <summary>
        /// To pre-validate the string to compare to
        /// </summary>
        /// <returns>Returns a boolean value to indiate whether the string to compare to is pre-validated</returns>
		protected override bool PreValidate()
		{
			bool result = true;

			// check datatypes
			if (ValueToCompare == null)
			{
				_errorMessage = "Value to compare is required.";
				result = false;
			}

			if (CompareToValue == null)
			{
				_errorMessage = "Value to compare to is required.";
				result = false;
			}

			return result;
        }

        #endregion
    }
}