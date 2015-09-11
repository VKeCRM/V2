//-----------------------------------------------------------------------
// <copyright file="CompareDateTimeValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace VKeCRM.Common.Validation
{
	/// <summary>
	/// Compares two DateTime values
	/// </summary>
	public class CompareDateTimeValidator : CompareValidatorBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CompareDateTimeValidator class
        /// </summary>
        public CompareDateTimeValidator()
		{
		}

        /// <summary>
        /// Initializes a new instance of the CompareDateTimeValidator class
        /// </summary>
        /// <param name="compareOperator">Operator used for comparison</param>
        /// <param name="valueToCompare">The first value to compare</param>
        /// <param name="compareToValue">The second value to compare</param>
		public CompareDateTimeValidator(ValidationCompareOperator compareOperator, object valueToCompare, object compareToValue)
		{
			_compareOperator = compareOperator;
			_valueToCompare = Convert.ToDateTime(valueToCompare);
			_compareToValue = Convert.ToDateTime(compareToValue);
		}

        /// <summary>
        /// Initializes a new instance of the CompareDateTimeValidator class
        /// </summary>
        /// <param name="compareOperator">Operator used for comparison</param>
        /// <param name="valueToCompare">The first value to compare</param>
        /// <param name="compareToValue">The second value to compare</param>
		public CompareDateTimeValidator(ValidationCompareOperator compareOperator, DateTime valueToCompare, DateTime compareToValue)
			: base(compareOperator, valueToCompare, compareToValue)
		{
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value to compare
        /// </summary>
        public new DateTime ValueToCompare
		{
			get 
            { 
                return (DateTime)base.ValueToCompare; 
            }

			set 
            { 
                base.ValueToCompare = value; 
            }
		}

        /// <summary>
        /// Gets or sets the value to compare to
        /// </summary>
		public new DateTime CompareToValue
		{
			get 
            { 
                return (DateTime)base.CompareToValue; 
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
			DateTime valueToCompare = (DateTime)_valueToCompare;
			DateTime compareToValue = (DateTime)_compareToValue;

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