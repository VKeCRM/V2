//-----------------------------------------------------------------------
// <copyright file="RangeDoubleValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace VKeCRM.Common.Validation
{
    /// <summary>
    /// Validate range of double values
    /// </summary>
    public class RangeDoubleValidator : RangeValidatorBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RangeDoubleValidator class
        /// </summary>
        public RangeDoubleValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RangeDoubleValidator class
        /// </summary>
        /// <param name="valueToCompare">Value to compare for double range</param>
        /// <param name="min">Minimum value for double</param>
        /// <param name="max">Maximum value for double</param>
        public RangeDoubleValidator(object valueToCompare, object min, object max)
        {
            _valueToCompare = Convert.ToDouble(valueToCompare);
            _min = Convert.ToDouble(min);
            _max = Convert.ToDouble(max);
        }

        /// <summary>
        /// Initializes a new instance of the RangeDoubleValidator class
        /// </summary>
        /// <param name="valueToCompare">Value to compare for double range</param>
        /// <param name="min">Minimum value for double</param>
        /// <param name="max">Maximum value for double</param>
        public RangeDoubleValidator(double valueToCompare, double min, double max) : base(valueToCompare, min, max)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value to compare 
        /// </summary>
        public new double ValueToCompare
        {
            get 
            { 
                return (double) base.ValueToCompare; 
            }

            set 
            { 
                base.ValueToCompare = value; 
            }
        }

        /// <summary>
        /// Gets or sets the minimum value for double
        /// </summary>
        public new double Min
        {
            get 
            { 
                return (double) base.Min; 
            }

            set 
            { 
                base.Min = value; 
            }
        }

        /// <summary>
        /// Gets or sets the minimum value for double
        /// </summary>
        public new double Max
        {
            get 
            { 
                return (double) base.Max; 
            }

            set 
            { 
                base.Max = value; 
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compare if the value falls between the minimum and maximum double range
        /// </summary>
        /// <returns>Returns a boolean value that indicates whether the value is within the double range</returns>
        protected override bool Compare()
        {
            double valueToCompare = (double) _valueToCompare;
            double min = (double) _min;
            double max = (double) _max;

            return valueToCompare >= min && valueToCompare <= max;
        }

        /// <summary>
        /// Pre-validate double
        /// </summary>
        /// <returns>Returns a boolean value that indicates whether the double value is pre-validated</returns>
        protected override bool PreValidate()
        {
            bool result = true;
            return result;
        }

        #endregion
    }
}