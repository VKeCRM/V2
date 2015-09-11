//-----------------------------------------------------------------------
// <copyright file="CompareBooleanValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace VKeCRM.Common.Validation
{
	/// <summary>
	/// Compares two boolean values.
	/// </summary>
	public class CompareBooleanValidator : CompareValidatorBase
    {
        #region Constructors
        /// <summary>
		/// Initializes a new instance of the CompareBooleanValidator class.
		/// </summary>
		public CompareBooleanValidator()
		{
		}

		/// <summary>
		/// Initializes a new instance of the CompareBooleanValidator class.
		/// </summary>
		/// <param name="valueToCompare">The first value to compare.</param>
		/// <param name="compareToValue">The second value to compare.</param>
		public CompareBooleanValidator(object valueToCompare, object compareToValue)
		{
			_compareOperator = ValidationCompareOperator.Equal;
			_valueToCompare = Convert.ToBoolean(valueToCompare.ToString());
			_compareToValue = Convert.ToBoolean(compareToValue.ToString());
		}

		/// <summary>
		/// Initializes a new instance of the CompareBooleanValidator class.
		/// </summary>
		/// <param name="valueToCompare">The first value to compare.</param>
		/// <param name="compareToValue">The second value to compare.</param>
		public CompareBooleanValidator(bool valueToCompare, bool compareToValue)
		{
			_compareOperator = ValidationCompareOperator.Equal;
			_valueToCompare = valueToCompare;
			_compareToValue = compareToValue;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether there is a first value to compare
        /// </summary>
		public new bool ValueToCompare
		{
			get 
            { 
                return (bool)base.ValueToCompare; 
            }

			set 
            { 
                base.ValueToCompare = value; 
            }
		}

        /// <summary>
        /// Gets or sets a value indicating whether there is a second value to compare to
        /// </summary>
		public new bool CompareToValue
		{
			get 
            { 
                return (bool)base.CompareToValue; 
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
			bool valueToCompare = (bool)_valueToCompare;
			bool compareToValue = (bool)_compareToValue;

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