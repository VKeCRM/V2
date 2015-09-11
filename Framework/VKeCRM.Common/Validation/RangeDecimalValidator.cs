//-----------------------------------------------------------------------
// <copyright file="RangeDecimalValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace VKeCRM.Common.Validation
{
    /// <summary>
    /// Validate range of decimal values
    /// </summary>
    public class RangeDecimalValidator : RangeValidatorBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RangeDecimalValidator class
        /// </summary>
        public RangeDecimalValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RangeDecimalValidator class
        /// </summary>
        /// <param name="valueToCompare">Value to compare for decimal range</param>
        /// <param name="min">Minimum value for decimal</param>
        /// <param name="max">Maximum value for decimal</param>
        public RangeDecimalValidator(object valueToCompare, object min, object max)
        {
            _valueToCompare = Convert.ToDecimal(valueToCompare);
            _min = Convert.ToDecimal(min);
            _max = Convert.ToDecimal(max);
        }

        /// <summary>
        /// Initializes a new instance of the RangeDecimalValidator class
        /// </summary>
        /// <param name="valueToCompare">Value to compare for decimal range</param>
        /// <param name="min">Minimum value for decimal</param>
        /// <param name="max">Maximum value for decimal</param>
        public RangeDecimalValidator(decimal valueToCompare, decimal min, decimal max) : base(valueToCompare, min, max)
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
                return (decimal) base.ValueToCompare; 
            }

            set 
            { 
                base.ValueToCompare = value; 
            }
        }

        /// <summary>
        /// Gets or sets the minimum value for decimal
        /// </summary>
        public new decimal Min
        {
            get 
            { 
                return (decimal) base.Min; 
            }

            set 
            { 
                base.Min = value; 
            }
        }

        /// <summary>
        /// Gets or sets the maximum value for decimal
        /// </summary>
        public new decimal Max
        {
            get 
            { 
                return (decimal) base.Max; 
            }

            set 
            { 
                base.Max = value; 
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compare if the value falls between the minimum and maximum decimal range
        /// </summary>
        /// <returns>Returns a boolean value that indicates whether the value is within the decimal range</returns>
        protected override bool Compare()
        {
            decimal valueToCompare = (decimal) _valueToCompare;
            decimal min = (decimal) _min;
            decimal max = (decimal) _max;

            return valueToCompare >= min && valueToCompare <= max;
        }

        /// <summary>
        /// Pre-validate decimal
        /// </summary>
        /// <returns>Returns a boolean value that indicates whether the decimal value is pre-validated</returns>
        protected override bool PreValidate()
        {
            bool result = true;
            return result;
        }

        #endregion
    }
}