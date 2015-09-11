//-----------------------------------------------------------------------
// <copyright file="AddressValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace VKeCRM.Common.Validation
{
	/// <summary>
	///		Validator class for checking name
	/// </summary>
	public class UserNameValidator : ValidatorBase
    {
        #region Fields
        /// <summary>
        ///		UserName to validate
        /// </summary>
        private string _userName = string.Empty;
        #endregion

        #region Constructors
        /// <summary>
        ///		Initializes a new instance of the UserNameValidator class
        /// </summary>
		public UserNameValidator()
		{
		}

        /// <summary>
		///		Initializes a new instance of the UserNameValidator class
        /// </summary>
		/// <param name="userName">
		///		Set the user name to validate
		/// </param>
		public UserNameValidator(string userName)
        {
			_userName = userName;
        }
        #endregion

		#region Properties
		/// <summary>
		///		Gets or sets the user name to validate
		/// </summary>
		public string UserName
		{
			get
			{
				return _userName;
			}

			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					_userName = value.Trim();
				}
			}
		}
		#endregion

        #region Methods
        /// <summary>
        ///		To validate the user name
        /// </summary>
        /// <returns>
		///		Returns a value indicating whether the user name is validated
		/// </returns>
		public override bool Validate()
		{
			bool result = false;
			try
			{
                if (PreValidate())
                {
					result = IsValidUserName();
                }
			}
			catch (System.Exception ex)
			{
				_errorMessage = ex.Message;
			}

			return result;
        }

        /// <summary>
        ///		To pre-validate the user name
        /// </summary>
        /// <returns>
		///		Returns a value indicating whether the user name is pre-validated
		/// </returns>
        protected override bool PreValidate()
        {
            bool result = true;
            _errorMessage = string.Empty;
            return result;
        }

        /// <summary>
        ///		To check if the user name is valid or not
        /// </summary>
        /// <returns>
		///		Returns a value indicating whether user name is valid
		/// </returns>
        private bool IsValidUserName()
        {
            bool result = true;

            _errorMessage = string.Empty;

			Regex filter = new Regex(@"^\w{4,15}$");

			if (!filter.Match(_userName).Success)
			{
				_errorMessage = "Please enter a valid user name.";
				result = false;
			}

            return result;
        }
        #endregion
    }
}