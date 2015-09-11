//-----------------------------------------------------------------------
// <copyright file="CompareIntegerValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace VKeCRM.Common.Validation
{
	/// <summary>
	/// Compares two integer values
	/// </summary>
	public class CompareIntegerValidator : CompareValidatorBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CompareIntegerValidator class
        /// </summary>
        public CompareIntegerValidator()
		{
		}

        /// <summary>
        /// Initializes a new instance of the CompareIntegerValidator class
        /// </summary>
        /// <param name="compareOperator">Operator used for comparison</param>
        /// <param name="valueToCompare">The first value to compare</param>
        /// <param name="compareToValue">The second value to compare</param>
		public CompareIntegerValidator(ValidationCompareOperator compareOperator, object valueToCompare, object compareToValue)
		{
			_compareOperator = compareOperator;
			_valueToCompare = Convert.ToInt32(valueToCompare);
			_compareToValue = Convert.ToInt32(compareToValue);
		}

        /// <summary>
        /// Initializes a new instance of the CompareIntegerValidator class
        /// </summary>
        /// <param name="compareOperator">Operator used for comparison</param>
        /// <param name="valueToCompare">The first value to compare</param>
        /// <param name="compareToValue">The second value to compare</param>
		public CompareIntegerValidator(ValidationCompareOperator compareOperator, int valueToCompare, int compareToValue)
			: base(compareOperator, valueToCompare, compareToValue)
		{
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value to compare
        /// </summary>
        public new int ValueToCompare
		{
			get 
            { 
                return (int)base.ValueToCompare; 
            }

			set 
            { 
                base.ValueToCompare = value; 
            }
		}

        /// <summary>
        /// Gets or sets the value to compare to
        /// </summary>
		public new int CompareToValue
		{
			get 
            { 
                return (int)base.CompareToValue; 
            }

			set 
            { 
                base.CompareToValue = value; 
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// To set the value to compare to
        /// </summary>
        /// <returns>Returns the value to compare to</returns>
        protected override int CompareTo()
		{
			int valueToCompare = (int)_valueToCompare;
			int compareToValue = (int)_compareToValue;

			return valueToCompare.CompareTo(compareToValue);
		}

        /// <summary>
        /// Pre-validate the result
        /// </summary>
        /// <returns>Returns a boolean value to indicate whether the result is pre-validated</returns>
		protected override bool PreValidate()
		{
			bool result = true;
			return result;
        }

        #endregion
    }
}