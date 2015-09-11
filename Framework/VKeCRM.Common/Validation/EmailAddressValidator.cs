//-----------------------------------------------------------------------
// <copyright file="EmailAddressValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace VKeCRM.Common.Validation
{
	/// <summary>
	/// To validate the email address
	/// </summary>
	public class EmailAddressValidator : ValidatorBase
	{
		#region Fields

		/// <summary>
		/// Email address to validate
		/// </summary>
		private string _emailAddress = string.Empty;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the EmailAddressValidator class
		/// </summary>
		public EmailAddressValidator()
		{
		}

		/// <summary>
		/// Initializes a new instance of the EmailAddressValidator class
		/// </summary>
		/// <param name="emailAddress">Email address to validate</param>
		public EmailAddressValidator(string emailAddress)
		{
			_emailAddress = emailAddress.Trim();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the email address to validate
		/// </summary>
		public string EmailAddress
		{
			get
			{
				return _emailAddress;
			}

			set
			{
				if (value != null)
				{
					_emailAddress = value.Trim();
				}
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// To validate the email address
		/// </summary>
		/// <returns>Returns a boolean value to indicate whether the email address is validated</returns>
		public override bool Validate()
		{
			bool result = false;
			try
			{
				if (PreValidate())
				{
					result = IsValidEmailAddress();
				}
			}
			catch (System.Exception ex)
			{
				_errorMessage = ex.Message;
			}

			return result;
		}

		/// <summary>
		/// To check if email address is valid
		/// </summary>
		/// <returns>Returns a boolean value to indicate whether the email address is valid</returns>
		protected bool IsValidEmailAddress()
		{
			bool result = true;
			//Regex filter = new Regex(@"^.+@.+\..{2,4}$");

			// VL. Uses the same regex as CommunityServer.
			Regex filter = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

			if (_emailAddress.EndsWith(".") || !filter.Match(_emailAddress).Success)
			{
				_errorMessage = "Please enter a valid email address.";
				result = false;
			}
			else
			{
				Regex illegalChars = new Regex(@"[\(\)\<\>\,\;\:\\\/\""\[\]]");
				if (illegalChars.Match(_emailAddress).Success)
				{
					_errorMessage = "The email address contains illegal characters.";
					result = false;
				}
			}

			return result;
		}

		/// <summary>
		/// Pre-validate the email address
		/// </summary>
		/// <returns>Returns a boolean value to indicate whether the email address is pre-validated</returns>
		protected override bool PreValidate()
		{
			bool result = true;

			// check datatypes
			if (_emailAddress == null || _emailAddress.Length == 0)
			{
				_errorMessage = "Please enter an email address.";
				result = false;
			}

			return result;
		}

		#endregion
	}
}