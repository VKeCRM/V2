//-----------------------------------------------------------------------
// <copyright file="RangeValidatorBase.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace VKeCRM.Common.Validation
{
    /// <summary>
    /// Base class for RangeValidator.
    /// </summary>
    public abstract class RangeValidatorBase : ValidatorBase
    {
        #region Fields

        /// <summary>
        /// Maximum value for range
        /// </summary>
        protected object _max;

        /// <summary>
        /// Minimum value for range
        /// </summary>
        protected object _min;

        /// <summary>
        /// Value to compare
        /// </summary>
        protected object _valueToCompare;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RangeValidatorBase class
        /// </summary>
        public RangeValidatorBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RangeValidatorBase class
        /// </summary>
        /// <param name="valueToCompare">Value to compare</param>
        /// <param name="min">Minimum value for range</param>
        /// <param name="max">Maximum value for range</param>
        public RangeValidatorBase(object valueToCompare, object min, object max)
        {
            _valueToCompare = valueToCompare;
            _min = min;
            _max = max;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value to compare 
        /// </summary>
        public object ValueToCompare
        {
            get 
            { 
                return _valueToCompare; 
            }

            set 
            { 
                _valueToCompare = value; 
            }
        }

        /// <summary>
        /// Gets or sets the minimum value
        /// </summary>
        public object Min
        {
            get 
            { 
                return _min; 
            }

            set 
            { 
                _min = value; 
            }
        }

        /// <summary>
        /// Gets or sets the maximum value
        /// </summary>
        public object Max
        {
            get 
            { 
                return _max; 
            }

            set 
            { 
                _max = value; 
            }
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Validates the value
        /// </summary>
        /// <returns>Returns a boolean value to indicate if the value is validated</returns>
        public override bool Validate()
        {
            bool result = false;
            try
            {
                if (PreValidate())
                {
                    result = Compare();
                }
            }
            catch (System.Exception ex)
            {
                _errorMessage = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// To compare if the value falls between the minimum and maximum range
        /// </summary>
        /// <returns>Returns a boolean value to indicate whether the values is within the range</returns>
        protected abstract bool Compare();

        #endregion
    }
}