//-----------------------------------------------------------------------
// <copyright file="CompareDecimalValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace VKeCRM.Common.Validation
{
	/// <summary>
    /// Compares two decimal values
	/// </summary>
	public class CompareDecimalValidator : CompareValidatorBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CompareDecimalValidator class
        /// </summary>
        public CompareDecimalValidator()
		{
		}

        /// <summary>
        /// Initializes a new instance of the CompareDecimalValidator class
        /// </summary>
        /// <param name="compareOperator">Operator used for comparison</param>
        /// <param name="valueToCompare">The first value to compare</param>
        /// <param name="compareToValue">The second value to compare</param>
		public CompareDecimalValidator(ValidationCompareOperator compareOperator, object valueToCompare, object compareToValue)
		{
			_compareOperator = compareOperator;
			_valueToCompare = Convert.ToDecimal(valueToCompare);
			_compareToValue = Convert.ToDecimal(compareToValue);
		}

        /// <summary>
        /// Initializes a new instance of the CompareDecimalValidator class
        /// </summary>
        /// <param name="compareOperator">Operator used for comparison</param>
        /// <param name="valueToCompare">The first value to compare</param>
        /// <param name="compareToValue">The second value to compare</param>
		public CompareDecimalValidator(ValidationCompareOperator compareOperator, decimal valueToCompare, decimal compareToValue)
			: base(compareOperator, valueToCompare, compareToValue)
		{
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value to compare
        /// </summary>
        public new decimal ValueToCompare
		{
			get 
            { 
                return (decimal)base.ValueToCompare; 
            }

			set 
            { 
                base.ValueToCompare = value; 
            }
		}

        /// <summary>
        /// Gets or sets the value to compare to
        /// </summary>
		public new decimal CompareToValue
		{
			get 
            { 
                return (decimal)base.CompareToValue; 
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
			decimal valueToCompare = (decimal)_valueToCompare;
			decimal compareToValue = (decimal)_compareToValue;

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