//-----------------------------------------------------------------------
// <copyright file="RangeDateTimeValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace VKeCRM.Common.Validation
{
    /// <summary>
    /// Validate range of date and time
    /// </summary>
    public class RangeDateTimeValidator : RangeValidatorBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RangeDateTimeValidator class
        /// </summary>
        public RangeDateTimeValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RangeDateTimeValidator class
        /// </summary>
        /// <param name="valueToCompare">Value to compare for date time range</param>
        /// <param name="min">Minimum value for date time</param>
        /// <param name="max">Maximum value for date time</param>
        public RangeDateTimeValidator(object valueToCompare, object min, object max)
        {
            _valueToCompare = Convert.ToDateTime(valueToCompare);
            _min = Convert.ToDateTime(min);
            _max = Convert.ToDateTime(max);
        }

        /// <summary>
        /// Initializes a new instance of the RangeDateTimeValidator class
        /// </summary>
        /// <param name="valueToCompare">Value to compare for date time range</param>
        /// <param name="min">Minimum value for date time</param>
        /// <param name="max">Maximum value for date time</param>
        public RangeDateTimeValidator(DateTime valueToCompare, DateTime min, DateTime max) : base(valueToCompare, min, max)
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
                return (DateTime) base.ValueToCompare; 
            }

            set 
            { 
                base.ValueToCompare = value; 
            }
        }

        /// <summary>
        /// Gets or sets the minimum value for datetime
        /// </summary>
        public new DateTime Min
        {
            get 
            { 
                return (DateTime) base.Min; 
            }

            set 
            { 
                base.Min = value; 
            }
        }

        /// <summary>
        /// Gets or sets the maximum value for datetime
        /// </summary>
        public new DateTime Max
        {
            get 
            { 
                return (DateTime) base.Max; 
            }

            set 
            { 
                base.Max = value; 
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compare if the value falls between the minimum and maximum datetime range
        /// </summary>
        /// <returns>Returns a boolean value that indicates whether the value is within the datetime range</returns>
        protected override bool Compare()
        {
            DateTime valueToCompare = (DateTime) _valueToCompare;
            DateTime min = (DateTime) _min;
            DateTime max = (DateTime) _max;

            return valueToCompare >= min && valueToCompare <= max;
        }

        /// <summary>
        /// Pre-validate datetime
        /// </summary>
        /// <returns>Returns a boolean value that indicates whether the datetime value is pre-validated</returns>
        protected override bool PreValidate()
        {
            bool result = true;

            return result;
        }

        #endregion
    }
}