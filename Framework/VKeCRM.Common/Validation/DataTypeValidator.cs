//-----------------------------------------------------------------------
// <copyright file="DataTypeValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace VKeCRM.Common.Validation
{
	/// <summary>
	/// Validator class for data types
	/// </summary>
	public class DataTypeValidator : ValidatorBase
    {
        #region Fields

        /// <summary>
        /// Data type to validate input against
        /// </summary>
        private string _dataType = string.Empty;

        /// <summary>
        /// Input object to validate data type
        /// </summary>
		private object _valueToCheck = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DataTypeValidator class
        /// </summary>
        public DataTypeValidator()
		{
		}

        /// <summary>
        /// Initializes a new instance of the DataTypeValidator class
        /// </summary>
        /// <param name="valueToCheck">Input value to validate</param>
		public DataTypeValidator(object valueToCheck)
		{
			_valueToCheck = valueToCheck;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the data type
        /// </summary>
        public string DataType
		{
			get 
            { 
                return _dataType; 
            }

			set
			{
				string dataType = value.ToLower();
                if (string.Equals("string", dataType) || string.Equals("integer", dataType) || string.Equals("decimal", dataType) || string.Equals("double", dataType) || string.Equals("datetime", dataType))
                {
                    _dataType = value;
                }
			}
		}

        /// <summary>
        /// Gets or sets the input value to check
        /// </summary>
		public object ValueToCheck
		{
			get 
            { 
                return _valueToCheck; 
            }

			set 
            { 
                _valueToCheck = value; 
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// To validate the data type
        /// </summary>
        /// <returns>Returns a boolean value to indicate if input value is validated</returns>
        public override bool Validate()
        {
            bool result = false;
            try
            {
                if (PreValidate())
                {
                    result = CheckType();
                }
            }
            catch (System.Exception ex)
            {
                _errorMessage = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// To check data type
        /// </summary>
        /// <returns>Returns a boolean value indicating that the data type is checked</returns>
        protected bool CheckType()
		{
			bool result = true;

			try
			{
				object testValue = null;

				string dataType = _dataType.ToLower();

                if (string.Equals("string", dataType))
                {
                    testValue = Convert.ToString(_valueToCheck);
                }
                else if (string.Equals("integer", dataType))
                {
                    testValue = Convert.ToInt32(_valueToCheck);
                }
                else if (string.Equals("decimal", dataType))
                {
                    testValue = Convert.ToDecimal(_valueToCheck);
                }
                else if (string.Equals("double", dataType))
                {
                    testValue = Convert.ToDouble(_valueToCheck);
                }
                else if (string.Equals("datetime", dataType))
                {
                    testValue = Convert.ToDateTime(_valueToCheck);
                }
                else
                {
                    result = false;
                }
			}
			catch (System.Exception ex)
			{
				_errorMessage = ex.ToString();
				result = false;
			}

			return result;
		}

        /// <summary>
        /// To pre-validate input value
        /// </summary>
        /// <returns>Returns a boolean value to indicate if input value is pre-validated</returns>
		protected override bool PreValidate()
		{
			bool result = true;

			if (_valueToCheck == null)
			{
				_errorMessage = "Value to check is required.";
				result = false;
			}

			return result;
		}

        #endregion
    }
}