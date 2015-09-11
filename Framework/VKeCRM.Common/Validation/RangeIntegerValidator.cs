//-----------------------------------------------------------------------
// <copyright file="RangeIntegerValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace VKeCRM.Common.Validation
{
    /// <summary>
    /// Validate range of Integer values
    /// </summary>
    public class RangeIntegerValidator : RangeValidatorBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RangeIntegerValidator class
        /// </summary>
        public RangeIntegerValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RangeIntegerValidator class
        /// </summary>
        /// <param name="valueToCompare">Value to compare for Integer range</param>
        /// <param name="min">Minimum value for Integer</param>
        /// <param name="max">Maximum value for Integer</param>
        public RangeIntegerValidator(object valueToCompare, object min, object max)
        {
            _valueToCompare = Convert.ToInt32(valueToCompare);
			_min = Convert.ToInt32(min);
			_max = Convert.ToInt32(max);
        }

        /// <summary>
        /// Initializes a new instance of the RangeIntegerValidator class
        /// </summary>
        /// <param name="valueToCompare">Value to compare for Integer range</param>
        /// <param name="min">Minimum value for Integer</param>
        /// <param name="max">Maximum value for Integer</param>
        public RangeIntegerValidator(int valueToCompare, int min, int max) : base(valueToCompare, min, max)
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
                return (int) base.ValueToCompare; 
            }

            set 
            { 
                base.ValueToCompare = value; 
            }
        }

        /// <summary>
        /// Gets or sets the minimum value for Integer
        /// </summary>
        public new int Min
        {
            get 
            { 
                return (int) base.Min; 
            }

            set 
            { 
                base.Min = value; 
            }
        }

        /// <summary>
        /// Gets or sets the maximum value for Integer
        /// </summary>
        public new int Max
        {
            get 
            { 
                return (int) base.Max; 
            }

            set 
            { 
                base.Max = value; 
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compare if the value falls between the minimum and maximum Integer range
        /// </summary>
        /// <returns>Returns a boolean value that indicates whether the value is within the Integer range</returns>
        protected override bool Compare()
        {
            int valueToCompare = (int) _valueToCompare;
            int min = (int) _min;
            int max = (int) _max;

            return valueToCompare >= min && valueToCompare <= max;
        }

        /// <summary>
        /// Pre-validate integer
        /// </summary>
        /// <returns>Returns a boolean value that indicates whether the integer value is pre-validated</returns>
        protected override bool PreValidate()
        {
            bool result = true;
            return result;
        }
        #endregion
    }
}